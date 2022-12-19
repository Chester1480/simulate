using Dapper;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace CIMSimulate.Service.UtilS
{
    public class DBService
    {
        private int defaultTimeout = 300;//預設timeout時間
        private ConfigService _configService;
        public DBService(IServiceProvider service)
        {
            _configService = service.GetService<ConfigService>()!;
        }

        #region ConnectionString 
        public string GetMyDBConString()
        {
            return _configService.GetIPCConString(); ;
        }
        #endregion

        /// <summary>
        /// Get DB ConnectionString
        /// </summary>
        /// <param name="databasename"></param>
        /// <returns></returns>
        public string GetDefaultConnectionString(string databasename = "")
        {
            string ConnectionString = "";

            if (string.IsNullOrWhiteSpace(databasename))
            {
                ConnectionString = GetMyDBConString();
            }
            else
                ConnectionString = databasename;

            return ConnectionString;
        }

        /// <summary>
        /// Mysql同步執行 可做新修刪
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>bool</returns>
        public bool MysqlExecute(string sql, DynamicParameters dynamics, string ConStr = "")
        {
            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                conn.Open();
                var result = conn.Execute(sql, dynamics);
                conn.Close();
                return result > 0;
            }
        }

        /// <summary>
        /// Mysql異步執行 可做新修刪
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>bool</returns>
        public async Task<bool> MysqlExecuteAsync(string sql, DynamicParameters dynamics, string ConStr = "")
        {

            #region example : 新增範例
            //string sql =   @"
            //                INSERT INTO Card 
            //                (
            //                    [Name]
            //                   ,[Description]
            //                   ,[Attack]
            //                   ,[Health]
            //                   ,[Cost]
            //                ) 
            //                VALUES 
            //                (
            //                    @Name
            //                   ,@Description
            //                   ,@Attack
            //                   ,@Health
            //                   ,@Cost
            //                );

            //                SELECT @@IDENTITY;
            //            ";
            #endregion
            #region example : 修改範例
            //string sql =@"
            //        UPDATE Card
            //        SET 
            //             [Name] = @Name
            //            ,[Description] = @Description
            //            ,[Attack] = @Attack
            //            ,[Health] = @Health
            //            ,[Cost] = @Cost
            //        WHERE 
            //            Id = @id
            //        ";
            //var parameters = new DynamicParameters(parameter);
            //parameters.Add("Id", id, System.Data.DbType.Int32);
            #endregion
            try
            {
                using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
                {
                    conn.Open();

                    var result = await conn.ExecuteAsync(sql, dynamics);
                    conn.Close();
                    return result > 0;
                }
            }
            catch (Exception e)
            {
                string debugPoint = e.Message;
                throw;
            }

        }

        /// <summary>
        /// Mysql同步執行 可做新修刪 多筆
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="list"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>
        public int MysqlExecuteMultiple(string sql, List<DynamicParameters> list, string ConStr = "")
        {

            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                conn.Open();
                var result = conn.Execute(sql, list);
                conn.Close();
                return result;
            }
        }

        /// <summary>
        /// Mysql異步執行 可做新修刪 多筆
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="list"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>
        public async Task<int> MysqlExecuteMultipleAsync(string sql, List<DynamicParameters> list, string ConStr = "")
        {

            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                conn.Open();
                var result = await conn.ExecuteAsync(sql, list);
                conn.Close();
                return result;
            }
        }

        /// <summary>
        /// Mysql異步執行 可做新修刪 多筆
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="list"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>
        public async Task<int> MysqlExecuteMultipleAsync<T>(string sql, List<T> list, string ConStr = "")
        {

            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                conn.Open();
                var result = await conn.ExecuteAsync(sql, list);
                conn.Close();
                return result;
            }
        }

        /// <summary>
        /// Mysql 取第一筆同步查詢
        /// </summary>
        /// <param name="sql">查詢字串</param>
        /// <param name="parameters">動態參數設置</param>
        /// <returns>dynamic</returns>
        public dynamic MysqlQueryFirstOrDefault(string sql, DynamicParameters parameters, string ConStr = "")
        {
            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                conn.Open();
                var result = conn.QueryFirstOrDefault<dynamic>(sql, parameters, commandTimeout: defaultTimeout);
                conn.Close();
                return result;
            }
        }

        /// <summary>
        /// Mysql 取第一筆同步查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>
        public T MysqlQueryFirstOrDefault<T>(string sql, DynamicParameters parameters, string ConStr = "")
        {
            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                conn.Open();
                var result = conn.QueryFirstOrDefault<T>(sql, parameters, commandTimeout: defaultTimeout);
                conn.Close();
                return result;
            }
        }

        /// <summary>
        /// Mysql 取第一筆異步查詢
        /// </summary>
        /// <param name="sql">查詢字串</param>
        /// <param name="parameters">動態參數設置</param>
        /// <returns>dynamic</returns>
        public async Task<dynamic> MysqlQueryFirstOrDefaultAsync(string sql, DynamicParameters parameters, string ConStr = "")
        {
            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                conn.Open();
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sql, parameters, commandTimeout: defaultTimeout);
                conn.Close();
                return result;
            }
        }

        /// <summary>
        /// Mysql 取第一筆異步查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>
        public async Task<T> MysqlQueryFirstOrDefaultAsync<T>(string sql, DynamicParameters parameters, string ConStr = "")
        {
            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                conn.Open();
                var result = await conn.QueryFirstOrDefaultAsync<T>(sql, parameters, commandTimeout: defaultTimeout);
                conn.Close();
                return result;
            }
        }

        /// <summary>
        /// Mysql多筆同步查詢
        /// </summary>
        /// <param name="sql">查詢字串</param>
        /// <param name="parameters">動態參數設置</param>
        /// <returns>IEnumerable<dynamic></returns>
        public IEnumerable<dynamic> MysqlQuery(string sql, DynamicParameters parameters, string ConStr = "")
        {
            #region example :
            // string sql = @"select * from User where id = ?"
            // parameters.Add("id", id);
            #endregion
            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                conn.Open();
                var result = conn.Query(sql, parameters, commandTimeout: defaultTimeout);
                conn.Close();
                return result;
            }
        }

        /// <summary>
        /// Mysql多筆同步查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>

        public IEnumerable<T> MysqlQuery<T>(string sql, DynamicParameters parameters, string ConStr = "")
        {
            #region example :
            // string sql = @"select * from User where id = ?"
            // parameters.Add("id", id);
            #endregion
            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                conn.Open();
                var result = conn.Query<T>(sql, parameters, commandTimeout: defaultTimeout);
                conn.Close();
                return result;
            }
        }

        /// <summary>
        /// Mysql多筆異步查詢
        /// </summary>
        /// <param name="sql">查詢字串</param>
        /// <param name="parameters">動態參數設置</param>
        /// <returns>IEnumerable<dynamic></returns>
        public async Task<IEnumerable<dynamic>> MysqlQueryAsync(string sql, DynamicParameters parameters, string ConStr = "")
        {
            #region example :
            // string sql = @"select * from User where id = ?"
            // parameters.Add("id", id);
            #endregion
            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                try
                {
                    conn.Open();
                    var result = await conn.QueryAsync(sql, parameters, commandTimeout: defaultTimeout);
                    conn.Close();
                    return result.AsEnumerable();
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    return null;
                }
            }
        }

        /// <summary>
        /// Mysql多筆異步查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> MysqlQueryAsync<T>(string sql, DynamicParameters parameters, string ConStr = "")
        {
            #region example :
            // string sql = @"select * from User where id = ?"
            // parameters.Add("id", id);
            #endregion
            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                try
                {
                    conn.Open();
                    var result = await conn.QueryAsync<T>(sql, parameters, commandTimeout: defaultTimeout);
                    conn.Close();
                    return result.AsEnumerable();
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    return null;
                }
            }
        }

        /// <summary>
        ///  Mysql 取單一筆同步查詢: return a single object
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>
        public dynamic MysqlQuerySingle(string sql, DynamicParameters parameters, string ConStr = "")
        {
            try
            {
                using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
                {
                    conn.Open();
                    var result = conn.QuerySingle<dynamic>(sql, parameters, commandTimeout: defaultTimeout);
                    conn.Close();
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Mysql 取單一筆同步查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>
        public T MysqlQuerySingle<T>(string sql, DynamicParameters parameters, string ConStr = "")
        {
            try
            {
                using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
                {
                    conn.Open();
                    var result = conn.QuerySingle<T>(sql, parameters, commandTimeout: defaultTimeout);
                    conn.Close();
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        ///  Mysql 單筆異步查詢: return a single object
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>
        public async Task<dynamic> MysqlQuerySingleAsync(string sql, DynamicParameters parameters, string ConStr = "")
        {
            try
            {
                using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
                {
                    conn.Open();
                    var result = await conn.QuerySingleAsync<dynamic>(sql, parameters, commandTimeout: defaultTimeout);
                    conn.Close();
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Mysql 單筆異步查詢: return a single object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>
        public async Task<T> MysqlQuerySingleAsync<T>(string sql, DynamicParameters parameters, string ConStr = "")
        {
            try
            {
                using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
                {
                    conn.Open();
                    var result = await conn.QuerySingleAsync<T>(sql, parameters, commandTimeout: defaultTimeout);
                    conn.Close();
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Mysql 同步查詢: return a DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>
        public DataTable MysqlQueryDataTable(string sql, DynamicParameters parameters, string ConStr = "")
        {
            try
            {
                using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
                {
                    using (var comm = new MySqlCommand(sql, conn))
                    {
                        conn.Open();

                        comm.Parameters.Add(parameters);

                        using (var reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataTable dt = new DataTable();
                                dt.Load(reader);
                                conn.Close();
                                return dt;
                            }
                            return null;
                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Mysql 異步查詢: return a DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>
        public async Task<DataTable> MysqlQueryDataTableAsync(string sql, DynamicParameters parameters, string ConStr = "")
        {
            try
            {
                using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
                {
                    using (var comm = new MySqlCommand(sql, conn))
                    {
                        conn.Open();

                        comm.Parameters.Add(parameters);

                        using (var reader = await comm.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                DataTable dt = new DataTable();
                                dt.Load(reader);
                                conn.Close();
                                return dt;
                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    //2022.12.19 - 將ConfigService 另建檔案
    //public class ConfigService
    //{

    //    #region DBConnectionString
    //    public string GetDBConString()
    //    {
    //        var builder = new ConfigurationBuilder()
    //              .SetBasePath(Directory.GetCurrentDirectory())
    //              .AddJsonFile("appsettings.json");
    //        var config = builder.Build();
    //        var connectionString = config.GetConnectionString("DBConnection");
    //        return connectionString;
    //    }
    //    #endregion

    //}
}
