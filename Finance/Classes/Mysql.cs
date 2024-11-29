using System.Data;
using MySqlConnector;

namespace Finance.Classes
{
    public class Mysql : IDisposable
    {
        public delegate void MessageEventHandler(string message);

        public static event MessageEventHandler ErrorEvent;

        static MySqlConnectionStringBuilder _connectionBuilder = new MySqlConnectionStringBuilder()
        { 
            Server = "FVH1.spaceweb.ru",
            Port = 3306,
            UserID = "daniilkupt",
            Password = "Ironmaiden01",
            Database = "daniilkupt",
            AllowUserVariables = true
        };

        public static MySqlConnectionStringBuilder ConnectionBuilder
        {
            get
            {
                return _connectionBuilder;
            }
            set
            {
                _connectionBuilder = value;
            }
        }

        MySqlConnection conn = null;
        MySqlTransaction transaction = null;

        public Mysql()
        {
            conn = new MySqlConnection(Mysql._connectionBuilder.ConnectionString);
        }

        public Mysql(string connectionString)
        {
            conn = new MySqlConnection(connectionString);
        }

        public Mysql(MySqlConnectionStringBuilder connectionStringBuilder)
        {
            conn = new MySqlConnection(connectionStringBuilder.ConnectionString);
        }

        public MySqlConnection Connection
        {
            get { return this.conn; }
        }

        void Connect()
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
        }

        void Disconnect()
        {
            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();
        }

        public void BeginTransaction()
        {
            if (conn.State != ConnectionState.Open)
                Connect();

            this.transaction = conn.BeginTransaction();

            return;
        }

        public void Commit()
        {
            if (this.transaction != null)
            {
                try
                {
                    this.transaction.Commit();
                }
                catch (MySqlException ex)
                {
                    if (Mysql.ErrorEvent != null)
                        Mysql.ErrorEvent(ex.Message);
                }

                this.transaction = null;
            }

            return;
        }

        public void Rollback()
        {
            if (this.transaction != null)
            {
                try
                {
                    this.transaction.Rollback();
                }
                catch (MySqlException ex)
                {
                    if (Mysql.ErrorEvent != null)
                        Mysql.ErrorEvent(ex.Message);
                }

                this.transaction = null;
            }

            return;
        }

        public object GetValue(string Qerry, Dictionary<string, object> whereClause = null)
        {
            if (conn.State != ConnectionState.Open)
                Connect();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = Qerry;

            if (whereClause != null)
            {
                foreach (KeyValuePair<string, object> kvp in whereClause)
                {
                    if (kvp.Value == null)
                        cmd.Parameters.AddWithValue(kvp.Key, DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
                }
            }

            object cel = null;
            try
            {
                cel = cmd.ExecuteScalar();
            }
            catch (MySqlException ex)
            {
                if (Mysql.ErrorEvent != null)
                    Mysql.ErrorEvent(ex.Message);
            }

            return cel;
        }

        public object GetValue(string Qerry, params object[] par)
        {
            return GetValue(string.Format(Qerry, par));
        }

        public Task<object> GetValueAsync(string Qerry)
        {
            return Task.Run(() => {
                return GetValue(Qerry);
            });
        }

        public DataTable GetTable(string sql, bool is_void = false)
        {
            if (conn.State != ConnectionState.Open)
                Connect();

            var dt = new DataTable();
            var da = new MySqlDataAdapter(sql, this.conn);
            try
            {
                da.Fill(dt);
            }
            catch (MySqlException ex)
            {
                dt = null;

                var error = ex.Message;

                switch (ex.SqlState)
                {
                    // TODO: Нать выцепить новые коды
                    case "55P03":
                        error = "Запись заблокирована другим процессом! Попробуйте позже";
                        break;

                    default:
                        break;
                }

                if (Mysql.ErrorEvent != null)
                    Mysql.ErrorEvent(error);
            }
            catch (ThreadAbortException ex)
            {

            }
            catch (Exception ex)
            {
            }

            if (!is_void && dt != null && dt.Rows.Count == 0)
                dt = null;

            return dt;
        }

        public Task<DataTable> GetTableAsync(string sql)
        {
            return Task.Run(() => {
                var dt = new DataTable();
                var connection = new MySqlConnection(ConnectionBuilder.ConnectionString);
                var da = new MySqlDataAdapter(sql, conn);
                try
                {
                    connection.OpenAsync();
                    da.Fill(dt);
                }
                catch (MySqlException)
                {
                }
                finally
                {
                    da.Dispose();
                    connection.Close();
                    connection.Dispose();
                }
                return dt;
            });
        }

        public DataRow GetRow(string sql)
        {
            var dt = new DataTable();
            Fill(dt, sql);
            return dt.Rows.Count != 0 ? dt.Rows[0] : null;
        }

        public Task<DataRow> GetRowAsync(string sql)
        {
            return Task.Run(() => {
                var dt = new DataTable();
                var connection = new MySqlConnection(ConnectionBuilder.ConnectionString);
                var da = new MySqlDataAdapter(sql, conn);

                try
                {
                    connection.Open();
                    da.Fill(dt);
                }
                catch (MySqlException)
                {
                }
                finally
                {
                    da.Dispose();
                    connection.Close();
                    connection.Dispose();
                }
                return dt.Rows.Count != 0 ? dt.Rows[0] : null;
            });
        }

        public List<object> GetColumn(string sql)
        {
            if (conn.State != ConnectionState.Open)
                Connect();

            var dt = new DataTable();
            var da = new MySqlDataAdapter(sql, this.conn);
            try
            {
                da.Fill(dt);
            }
            catch (MySqlException ex)
            {
                dt = null;
                if (Mysql.ErrorEvent != null)
                    Mysql.ErrorEvent(ex.Message);
            }
            if (dt != null)
                return dt.AsEnumerable().Select(r => r.Field<object>(0)).ToList();
            else
                return new List<object>();
        }

        public void Fill(DataTable table, string query)
        {

            DataTable dt = GetTable(query, true);

            if (dt == null)
                return;

            table.Clear();
            table.PrimaryKey = null;
            table.Columns.Clear();
            table.Merge(dt, false, MissingSchemaAction.Add);
            table.AcceptChanges();
        }

        public void ExecSql(string Qerry)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    Connect();

                var cmd = new MySqlCommand(Qerry, conn);

                var rowsaffected = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                if (Mysql.ErrorEvent != null)
                    Mysql.ErrorEvent(ex.Message);
            }
            catch (Exception ex)
            {
                if (Mysql.ErrorEvent != null)
                    Mysql.ErrorEvent(ex.Message);
            }
        }

        public void ExecSql(string Qerry, params object[] par)
        {
            ExecSql(string.Format(Qerry, par));
        }

        public void ExecSql(string Qerry, MySqlParameter[] parametrs, int cntWork = 1)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    Connect();

                var cmd = new MySqlCommand(Qerry, conn);
                cmd.Parameters.AddRange(parametrs);

                for (int i = 0; i < cntWork; i++) cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                if (Mysql.ErrorEvent != null)
                    Mysql.ErrorEvent(ex.Message);
            }
            catch (Exception ex)
            {
                if (Mysql.ErrorEvent != null)
                    Mysql.ErrorEvent(ex.Message);
            }
        }

        public Task<bool> ExecSqlAsync(string Qerry, params object[] par)
        {
            return Task.Run(() => {
                ExecSql(Qerry, par);
                return true;
            });
        }

        public Task<bool> ExecSqlAsync(string Qerry, MySqlParameter[] parametrs)
        {
            return Task.Run(() => {
                ExecSql(Qerry, parametrs);
                return true;
            });
        }

        public void UpdateBinaryColumn(string table, string column, string whereClouse, byte[] data)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    Connect();

                var cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = string.Format(@"UPDATE {0} SET {1}=@data WHERE {2}", table, column, whereClouse);
                cmd.Parameters.AddWithValue("@data", data);

                var rowsaffected = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                if (Mysql.ErrorEvent != null)
                    Mysql.ErrorEvent(ex.Message);
            }
            catch (Exception ex)
            {
                if (Mysql.ErrorEvent != null)
                    Mysql.ErrorEvent(ex.Message);
            }
        }

        public DataTable Insert(string table, Dictionary<string, object> pars, string returning = "")
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    Connect();

                var cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = String.Format(
                    @"INSERT INTO {0} ({1}) VALUES ({2}) {3};",
                    table,
                    String.Join(", ", pars.Select(x => x.Key).ToArray()),
                    String.Join(", ", pars.Select(x => "@" + x.Key).ToArray()),
                    returning.Trim() == "" ? "" : "RETURNING " + returning.Trim()
                );

                foreach (KeyValuePair<string, object> kvp in pars)
                {
                    if (kvp.Value == null)
                        cmd.Parameters.AddWithValue("@"+kvp.Key, DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@"+kvp.Key, kvp.Value);
                }

                var dt = new DataTable();
                var da = new MySqlDataAdapter(cmd);

                da.Fill(dt);

                if (dt != null && dt.Rows.Count == 0)
                    dt = null;

                return dt;

            }
            catch (MySqlException ex)
            {
                if (Mysql.ErrorEvent != null)
                    Mysql.ErrorEvent(ex.Message);
                return new DataTable();
            }
            catch (Exception ex)
            {
                if (Mysql.ErrorEvent != null)
                    Mysql.ErrorEvent(ex.Message);
                return new DataTable();
            }
        }

        public DataTable Update(string table, Dictionary<string, object> pars, string whereClause, string returning = "")
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    Connect();

                var cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = String.Format(
                    @"UPDATE {0} SET {1} WHERE {2} {3};",
                    table,
                    String.Join(", ", pars.Select(x => x.Key + "=" + "@" + x.Key).ToArray()),
                    whereClause,
                    returning.Trim() == "" ? "" : "RETURNING " + returning.Trim()
                );

                foreach (KeyValuePair<string, object> kvp in pars)
                {
                    cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
                }

                var dt = new DataTable();
                var da = new MySqlDataAdapter(cmd);

                da.Fill(dt);

                if (dt != null && dt.Rows.Count == 0)
                    dt = null;

                return dt;
            }
            catch (MySqlException ex)
            {
                if (Mysql.ErrorEvent != null)
                    Mysql.ErrorEvent(ex.Message);
            }
            catch (Exception ex)
            {
                if (Mysql.ErrorEvent != null)
                    Mysql.ErrorEvent(ex.Message);
            }

            return null;
        }

        public void Update(string table, Dictionary<string, object> pars, Dictionary<string, object> whereClause)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    Connect();

                var cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = string.Format(@"UPDATE {0} SET {1} WHERE {2};",
                    table,
                    string.Join(", ", pars.Select(x => x.Key + "=" + "@" + x.Key).ToArray()),
                    string.Join(" AND ", whereClause.Select(x => x.Key + "=" + "@w_" + x.Key).ToArray())
                );

                foreach (KeyValuePair<string, object> kvp in pars)
                    if (kvp.Value == null)
                        cmd.Parameters.AddWithValue("@" + kvp.Key, DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);

                foreach (KeyValuePair<string, object> kvp in whereClause)
                    if (kvp.Value == null)
                        cmd.Parameters.AddWithValue("@w_" + kvp.Key, DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@w_" + kvp.Key, kvp.Value);

                var rowsaffected = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                if (Mysql.ErrorEvent != null)
                    Mysql.ErrorEvent(ex.Message);
            }
            catch (Exception ex)
            {
                if (Mysql.ErrorEvent != null)
                    Mysql.ErrorEvent(ex.Message);
            }
        }

        public DataTable Call(string function)
        {
            var pars = new Dictionary<string, object>();
            return Call(function, pars);
        }

        public DataTable Call(string function, Dictionary<string, object> pars)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    Connect();

                var cmd = new MySqlCommand(function, conn);

                cmd.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, object> kvp in pars)
                    cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);

                var dt = new DataTable();
                var da = new MySqlDataAdapter(cmd);

                da.Fill(dt);
                return dt;

            }
            catch (MySqlException ex)
            {
                if (Mysql.ErrorEvent != null)
                    Mysql.ErrorEvent(ex.Message);
            }
            catch (Exception ex)
            {
                if (Mysql.ErrorEvent != null)
                    Mysql.ErrorEvent(ex.Message);
            }

            return null;
        }

        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Disconnect();
            }

            disposed = true;
        }
    }
}
