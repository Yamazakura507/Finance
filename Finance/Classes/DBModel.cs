//using Java.Util.Logging;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using MySqlConnector;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;

namespace Finance.Classes
{
    public class DBModel
    {
        protected static bool IsGet { get; set; } = false;

        public static void InsertModel<T>(Dictionary<string, object> parametrs)
        {
            try
            {
                CheckPolice(false, typeof(T));

                using (var ms = new Mysql())
                {
                    ms.Insert(typeof(T).Name, parametrs);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            try
            {
                CheckPolice(false, typeof(T));

                using (var ms = new Mysql())
                {
                    ms.Update(typeof(T).Name, parametrs, Id is null ? WhereCollection : new Dictionary<string, object>() { { "Id", Id } });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ObservableCollection<T> GetCollectionModel<T>(Dictionary<string, object>? WhereCollection = null, int Limit = 0, int Offset = 0, Dictionary<string, OrderType>? OrderCollection = null) where T : new()
        {
            try
            {
                CheckPolice(true, typeof(T));

                ObservableCollection<T> collection = new ObservableCollection<T>();

                using (var ms = new Mysql())
                {
                    var sql = @$"SELECT * FROM `{typeof(T).Name}`
                    WHERE {(WhereCollection is null ? "true" : String.Join(" AND ", WhereCollection.Select(i => $"`{i.Key}` = '{i.Value}'")))} 
                    {(OrderCollection is null ? null : $" ORDER BY {String.Join(", ", OrderCollection.Select(i => $"`{i.Key}` {(i.Value.OrderString())}"))}")} 
                    {(Limit == 0 ? Offset == 0 ? null : $"OFFSET {Offset}" : Offset == 0 ? $"LIMIT {Limit}" : $"LIMIT {Limit} OFFSET {Offset}")}";
                        var dt = ms.GetTable(sql.Trim());

                    if (dt is null) return null;

                    IsGet = true;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        collection.Add(dt.Rows[i].ToObject(new T()));
                    }

                    IsGet = false;
                }

                return collection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ObservableCollection<T> GetCollectionModel<T>(string sqlQuery) where T : new()
        {
            CheckPolice(true, typeof(T));

            try
            {
                ObservableCollection<T> collection = new ObservableCollection<T>();

                using (var ms = new Mysql())
                {
                    var dt = ms.GetTable(sqlQuery.Trim());

                    if (dt is null) return null;

                    IsGet = true;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        collection.Add(dt.Rows[i].ToObject(new T()));
                    }

                    IsGet = false;
                }

                return collection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Работает быстрее чем GetCollectionModel, но игнорирует процес сортировки!
        /// </summary>
        /// <typeparam name="T">Тип модели</typeparam>
        /// <param name="sqlQuery">Запрос к базе</param>
        /// <returns>Динамическую колекцию соответствующую выгрузке из базы</returns>
        public static ObservableCollection<T> GetParallelCollectionModel<T>(string sqlQuery) where T : new()
        {
            CheckPolice(true, typeof(T));

            try
            {
                ObservableCollection<T> collection = new ObservableCollection<T>();

                using (var ms = new Mysql())
                {
                    var dt = ms.GetTable(sqlQuery.Trim());

                    if (dt is null) return null;

                    IsGet = true;

                    Parallel.ForEach(dt.AsEnumerable(), dr => collection.Add(dr.ToObjectParallel(new T())));

                    IsGet = false;
                }

                return collection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T GetModel<T>(int? Id = null, string proc_comm = null,string errMess = null, int numRow = 1) where T : new()
        {
            try
            {
                CheckPolice(true, typeof(T));

                if (Id is null && proc_comm is null && errMess != null)
                    throw new Exception(errMess);

                DataRow dr = null;

                using (var ms = new Mysql())
                {
                    dr = ms.GetRow(Id is null ? proc_comm : $"SELECT * FROM `{typeof(T).Name}` WHERE {(Id is null ? "true" : $"`Id` = '{Id}'")} LIMIT 1 OFFSET {numRow - 1}");
                }

                if (dr is null)
                    throw new Exception(errMess);

                bool isGet = IsGet;
                IsGet = true;

                T obj = dr.ToObject<T>(new T());

                IsGet = isGet;

                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void DeleteModel<T>(int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            try
            {
                CheckPolice(false, typeof(T));

                using (var ms = new Mysql())
                {
                    ms.ExecSql(@$"DELETE FROM `{typeof(T).Name}`
                WHERE {(Id is null ? WhereCollection is null ? "true" : String.Join(", ", WhereCollection.Select(i => $"`{i.Key}` = '{i.Value}'")) : $"`Id` = '{Id}'")}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public virtual T GetParametrs<T>(string param, Type typeTb, int? Id = null)
        {
            try
            {
                CheckPolice(true, typeTb);

                T obj = default(T);

                using (var ms = new Mysql())
                {
                    object ob = ms.GetValue($"SELECT `{param}` FROM `{typeTb.Name}` WHERE `Id` = '{Id}'");
                    obj = (T)(ob == DBNull.Value ? null : ob);
                }

                return obj;
            }
            catch (Exception ex)
            {
                GC.Collect();
                return default(T);
            }
        }

        private static Type[] ignorePoliceType = { typeof(View.AssetsGroupChart),  typeof(View.CALL.view_future_payments_tmp)};

        public static void CheckPolice(bool isRead, Type typeTb, bool isAdmin = false)
        {
            try
            {
                if (ignorePoliceType.Contains(typeTb)) return;

                if (InfoAccount.IdUser > 0)
                {
                    string? msg = null;

                    using (var ms = new Mysql())
                    {
                        msg = ms.GetValue($@"SELECT GROUP_CONCAT(m.`msg` SEPARATOR '\n') msg 
                                                FROM (
                                                    SELECT CASE 
                                                                WHEN COUNT(*) = 0 
                                                                    THEN 'У вас нет прав {(isRead ? "чтения" : "записи")} объекта {typeTb.Name}!\nДля получения прав обратитесь в подержку' 
                                                                WHEN COUNT(*) <> COUNT(CASE WHEN 'WAR' LIKE CONCAT('%',obr.`Name`,'%') THEN 1 END) 
                                                                    THEN CONCAT('У вас нет прав {(isRead ? "чтения" : "записи")} объекта ',tn.Name,'!\nДля получения прав обратитесь в подержку')
                                                                WHEN '{isAdmin}' AND COUNT(*) <> COUNT(CASE WHEN obr.`Name` LIKE 'A' THEN 1 END) 
                                                                    THEN CONCAT('У вас нет прав администрирования объекта ',tn.Name,'!\nДля получения прав обратитесь в подержку')
                                                                ELSE NULL END AS msg
                                                    FROM `RestrictionsUser` ru 
                                                        INNER JOIN `GroupingRestriction` gr ON gr.`IdRestriction` = ru.`IdRestrictions` 
                                                        INNER JOIN `ObjectRestrict` obr ON obr.`Id` = gr.`IdObjectRestriction` 
                                                        INNER JOIN `GroupingObject` gro ON gr.`IdGroup` = gro.`IdGroup` 
                                                        INNER JOIN `TableName` tn ON tn.`Id` = gro.`IdObject`
                                                    WHERE ru.`IdUser` = '{InfoAccount.IdUser}' AND tn.`ObjectName` = '{typeTb.Name}'
                                                    ) m").ToString();
                    }

                    if (!String.IsNullOrEmpty(msg)) throw new Exception(msg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }    
        }

        public virtual void SetParametrs<T>(string param, object value, int? Id = null)
        {
            try
            {
                CheckPolice(false, typeof(T));

                using (var ms = new Mysql())
                {
                    ms.ExecSql($"UPDATE `{typeof(T).Name}` SET `{param}` = @val WHERE `Id` = @id", new []{ new MySqlParameter("@val", value), new MySqlParameter("@id", Id) });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object ResultRequest(string sql)
        {
            try
            {
                object obj;

                using (var ms = new Mysql())
                {
                    obj = ms.GetValue(sql);
                }

                return obj == DBNull.Value ? null : obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int Counter<T>(Dictionary<string, object>? WhereCollection = null, bool isUserCheck = true, bool distinct = false)
        {
            string sql = $"SELECT COUNT({(distinct ? "DISTINCT " : null)}t.`Id`) FROM `{typeof(T).Name}` t WHERE {(isUserCheck ? $"t.`IdUser` = '{InfoAccount.IdUser}' AND " : null)}{(WhereCollection is null ? "true" : String.Join(" AND ", WhereCollection.Select(i => $"t.`{i.Key}` = '{i.Value}'")))}";

            return new Mysql().GetValue<int>(sql);
        }

        public static List<G> GetColumn<T,G>(string column, Dictionary<string, object>? WhereCollection = null, bool isUserCheck = false, bool distinct = false)
        {
            List<object> listColumn;

            using (Mysql ms = new Mysql())
            {
                var sql = $"SELECT {(distinct ? "DISTINCT " : null)}`{column}` FROM `{typeof(T).Name}` WHERE {(isUserCheck ? $"t.`IdUser` = '{InfoAccount.IdUser}' AND " : null)}{(WhereCollection is null ? "true" : String.Join(" AND ", WhereCollection.Select(i => $"t.`{i.Key}` = '{i.Value}'")))}";
               listColumn = ms.GetColumn($"SELECT {(distinct ? "DISTINCT " : null)}`{column}` FROM `{typeof(T).Name}` t WHERE {(isUserCheck ? $"t.`IdUser` = '{InfoAccount.IdUser}' AND " : null)}{(WhereCollection is null ? "true" : String.Join(" AND ", WhereCollection.Select(i => $"t.`{i.Key}` = '{i.Value}'")))}");
            }

            return listColumn.Cast<G>().ToList();
        }

        public static G Serch<T,G>(string returnColumn, KeyValuePair<string, object> serchPair, bool is_like = false)
        {
            G result;

            using (Mysql ms = new Mysql())
            {
                result = ms.GetValue<G>($"SELECT `{returnColumn}` FROM `{typeof(T).Name}` WHERE {serchPair.Key} {(is_like ? $"LIKE '%{serchPair.Value}%'" : $"= '{serchPair.Value}'")} LIMIT 1");
            }

            return result;
        }

        public static string ConvertToMySqlDate(DateTime value) => value.ToString("yyyy-MM-dd HH:mm:ss").Replace(" ", "T");
        public static string ConvertToMySqlDecimal(decimal value) => value.ToString().Replace(",", ".");
        public static string ConvertToMySqlDecimal(string value) => value.Replace(",", ".");
    }
}
