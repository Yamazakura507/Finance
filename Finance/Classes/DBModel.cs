//using Java.Util.Logging;
using MySqlConnector;
using Finance.Classes.AppSettings;
using System.Collections.ObjectModel;
using System.Data;

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

        public static ObservableCollection<T> GetCollectionModel<T>(Dictionary<string, object>? WhereCollection = null, int Limit = 0, int Offset = 0, Dictionary<string, bool>? OrderCollection = null) where T : new()
        {
            try
            {
                CheckPolice(true, typeof(T));

                ObservableCollection<T> collection = new ObservableCollection<T>();

                using (var ms = new Mysql())
                {
                    var sql = @$"SELECT * FROM `{typeof(T).Name}`
                    WHERE {(WhereCollection is null ? "true" : String.Join(" AND ", WhereCollection.Select(i => $"`{i.Key}` = '{i.Value}'")))} 
                    {(OrderCollection is null ? null : $" ORDER BY {String.Join(", ", OrderCollection.Select(i => $"`{i.Key}` {(i.Value ? "asc" : "desc")}"))}")} 
                    {(Limit == 0 ? null : Offset == 0 ? $"LIMIT {Limit}" : $"LIMIT {Limit} OFFSET {Offset}")}";
                        var dt = ms.GetTable(sql.Trim());

                    if (dt is null) return null;

                    IsGet = true;

                    Parallel.ForEach(dt.AsEnumerable(), dr => collection.Add(dr.ToObject(new T())));

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

                    Parallel.ForEach(dt.AsEnumerable(), dr => collection.Add(dr.ToObject<T>(new T())));

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
                throw ex;
            }
        }

        public static void CheckPolice(bool isRead, Type typeTb, bool isAdmin = false)
        {
            try
            {
                if (InfoAccount.IdUser > 0)
                {
                    DataTable dtPolice = new DataTable();

                    using (var ms = new Mysql())
                    {
                        dtPolice = ms.GetTable($@"SELECT tn.`ObjectName`, tn.`Name`, obr.`Name` PoliceName FROM `RestrictionsUser` ru 
                                                    INNER JOIN `GroupingRestriction` gr ON gr.`IdRestriction` = ru.`IdRestrictions` 
                                                    INNER JOIN `ObjectRestrict` obr ON obr.`Id` = gr.`IdObjectRestriction` 
                                                    INNER JOIN `GroupingObject` gro ON gr.`IdGroup` = gro.`IdGroup` 
                                                    INNER JOIN `TableName` tn ON tn.`Id` = gro.`IdObject`
                                                WHERE ru.`IdUser` = '{InfoAccount.IdUser}' AND tn.`ObjectName` = '{typeTb.Name}'", true);
                    }

                    if (dtPolice is null) throw new Exception($"Увас нет прав {(isRead ? "чтения" : "записи")} объекта {typeTb.Name}!\nДля получения прав обратитесь в подержку");
                    if (!dtPolice.AsEnumerable().All(i =>  (isRead ? "WAR" : "WA").Contains(i["PoliceName"].ToString()))) throw new Exception($"Увас нет прав {(isRead ? "чтения" : "записи")} объекта {dtPolice.Rows[0]["Name"]}!\nДля получения прав обратитесь в подержку");
                    if (isAdmin && dtPolice.AsEnumerable().All(i =>  i["PoliceName"].ToString().Contains("A"))) throw new Exception($"Увас нет прав администрирования объекта {dtPolice.Rows[0]["Name"]}!\nДля получения прав обратитесь в подержку");
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

        public static object ResultRequest(string sql, Dictionary<string, object> whereClause = null)
        {
            try
            {
                object obj;

                using (var ms = new Mysql())
                {
                    obj = ms.GetValue(sql, whereClause);
                }

                return obj == DBNull.Value ? null : obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string ToFirstUpper(string str) => char.ToUpper(str[0]) + str.Substring(1);

        public static string ConvertToMySqlDate(DateTime value) => value.ToString("yyyy-MM-dd HH:mm:ss").Replace(" ", "T");
        public static string ConvertToMySqlDecimal(decimal value) => value.ToString().Replace(",", ".");
        public static string ConvertToMySqlDecimal(string value) => value.Replace(",", ".");
    }
}
