using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace KDTVN_Shared.Helper
{
    class SQLHelper
    {
        public string ConnectionString { get; set; }
        public SQLHelper(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public QueryResult BulkCopy(DataTable dtSource, string destTableName, List<string> sourceHeader, List<string> destHeader)
        {
            QueryResult qr = new QueryResult(-1, "FAIL");

            if (sourceHeader.Count.Equals(destHeader.Count))
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        cn.Open();
                        using (SqlBulkCopy copy = new SqlBulkCopy(cn))
                        {
                            for (int i = 0; i < sourceHeader.Count; i++)
                            {
                                copy.ColumnMappings.Add(sourceHeader[i], destHeader[i]);
                            }

                            copy.DestinationTableName = destTableName;
                            copy.WriteToServer(dtSource);
                        }

                        qr.Code = 1;
                        qr.Msg = "OK";
                    }
                    catch (Exception) { }
                    finally
                    {
                        cn.Close();
                    }
                }
            }
            else
            {
                return new QueryResult(-1, "Two list header not equal!");
            }

            return qr;
        }

        public QueryResult BulkCopy(DataTable dtSource, string destTableName)
        {
            QueryResult qr = new QueryResult(-1, "FAIL");

            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                try
                {
                    cn.Open();
                    using (SqlBulkCopy copy = new SqlBulkCopy(cn))
                    {
                        copy.DestinationTableName = destTableName;
                        copy.BulkCopyTimeout = 0;

                        copy.WriteToServer(dtSource);
                    }

                    qr.Code = 1;
                    qr.Msg = "OK";
                }
                catch (Exception ex)
                {
                    qr.Msg = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }

            return qr;
        }

        public long Insert<T>(T entityToInsert) where T : class
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                long re = 0;
                try
                {
                    re = connection.Insert(entityToInsert);
                }
                catch (Exception ex)
                {
                    re = 0;
                    Console.WriteLine(ex.Message);
                }
                return re;
            }
        }

        public bool Update<T>(T entityToUpdate) where T : class
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = false;
                try
                {
                    result = connection.Update(entityToUpdate);
                }
                catch (Exception ex)
                {
                    result = false;
                    Console.WriteLine(ex.Message);
                }
                return result;
            }
        }

        public bool Delete<T>(T entityToDelete) where T : class
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                bool success = false;
                try
                {
                    success = connection.Delete(entityToDelete);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return success;
            }
        }

        public DataTable ExecProcedureDataAsDataTable(string ProcedureName, object parametter = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                DataTable table = new DataTable();
                try
                {
                    var reader = connection.ExecuteReader(ProcedureName, param: parametter, commandType: CommandType.StoredProcedure);

                    table.Load(reader);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message, "Lỗi trả về từ Server: ");
                }

                return table;
            }
        }

        public async Task<DataTable> ExecProcedureDataAsyncAsDataTable(string ProcedureName, object parametter = null)
        {
            return await WithConnection(async c =>
            {
                var reader = await c.ExecuteReaderAsync(ProcedureName, param: parametter, commandType: CommandType.StoredProcedure);
                DataTable table = new DataTable();
                table.Load(reader);
                return table;
            });

        }

        public DataTable ExecQueryDataAsDataTable(string T_SQL, object parametter = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var reader = connection.ExecuteReader(T_SQL, param: parametter, commandType: CommandType.Text);
                DataTable table = new DataTable();
                table.Load(reader);
                return table;
            }
        }

        public async Task<DataTable> ExecQueryDataAsyncAsDataTable(string T_SQL, object parametter = null)
        {
            return await WithConnection(async c =>
            {
                var reader = await c.ExecuteReaderAsync(T_SQL, param: parametter, commandType: CommandType.Text);
                DataTable table = new DataTable();
                table.Load(reader);
                return table;
            });

        }

        public IEnumerable<T> ExecProcedureData<T>(string ProcedureName, object parametter = null)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return connection.Query<T>(ProcedureName, param: parametter, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + " : " + ex.Message);
            }
            return null;
        }

        public T ExecProcedureDataFistOrDefault<T>(string ProcedureName, object parametter = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<T>(ProcedureName, parametter, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<T>> ExecProcedureDataAsync<T>(string ProcedureName, object parametter = null)
        {

            return await WithConnection(async c =>
            {
                return await c.QueryAsync<T>(ProcedureName, parametter, commandType: CommandType.StoredProcedure);
            });


        }

        public T ExecProcedureDataFirstOrDefaultAsync<T>(string ProcedureName, object parametter = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefaultAsync<T>(ProcedureName, parametter, commandType: CommandType.StoredProcedure).Result;
            }
        }

        public int ExecProcedureNonData(string ProcedureName, object parametter = null)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    //return affectedRows 
                    return connection.Execute(ProcedureName, parametter, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int ExecProcedureNonDataAsync(string ProcedureName, object parametter = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                //return affectedRows 
                return connection.ExecuteAsync(ProcedureName, parametter, commandType: CommandType.StoredProcedure).Result;
            }
        }

        public IEnumerable<T> ExecQueryData<T>(string T_SQL, object parametter = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.Query<T>(T_SQL, parametter, commandType: CommandType.Text);
            }
        }

        public T ExecQueryDataFistOrDefault<T>(string T_SQL, object parametter = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<T>(T_SQL, parametter, commandType: CommandType.Text);
            }
        }

        public async Task<IEnumerable<T>> ExecQueryDataAsync<T>(string T_SQL, object parametter = null)
        {
            return await WithConnection(async c =>
            {
                return await c.QueryAsync<T>(T_SQL, parametter, commandType: CommandType.Text);
            });
        }

        public T ExecQueryDataFirstOrDefaultAsync<T>(string T_SQL, object parametter = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefaultAsync<T>(T_SQL, parametter, commandType: CommandType.Text).Result;
            }
        }

        public int ExecQueryNonData(string T_SQL, object parametter = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.Execute(T_SQL, parametter, commandType: CommandType.Text);
            }
        }

        public async Task<int> ExecQueryNonDataAsync(string T_SQL, object parametter = null)
        {
            return await WithConnection(async c =>
            {
                return await c.ExecuteAsync(T_SQL, parametter, commandType: CommandType.Text);
            });
        }

        public async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync(); // Asynchronously open a connection to the database
                return await getData(connection); // Asynchronously execute getData, which has been passed in as a Func<IDBConnection, Task<T>>
            }
        }

        public object ExecProcedureSacalar(string ProcedureName, object parametter = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.ExecuteScalar<object>(ProcedureName, parametter, commandType: CommandType.StoredProcedure);
            }

        }

        public object ExecProcedureSacalarAsync(string ProcedureName, object parametter = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.ExecuteScalarAsync<object>(ProcedureName, parametter, commandType: CommandType.StoredProcedure).Result;
            }

        }

        public object ExecQuerySacalar(string T_SQL, object parametter = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.ExecuteScalar<object>(T_SQL, parametter, commandType: CommandType.Text);
            }

        }

        public object ExecQuerySacalarAsync(string T_SQL, object parametter = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.ExecuteScalarAsync<object>(T_SQL, parametter, commandType: CommandType.Text).Result;
            }

        }

        public QueryResult ExecProcedureDataQueryResult(string ProcedureName, object parametter = null)
        {
            QueryResult qr = new QueryResult();
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return connection.Query<QueryResult>(ProcedureName, param: parametter, commandType: CommandType.StoredProcedure).First();

                   // return connection.Query<QueryResult>(ProcedureName, param: parametter, commandTimeout: 0, commandType: CommandType.StoredProcedure).First();
                }
            }
            catch (Exception ex)
            {
                qr = new QueryResult(ex);
            }
            return qr;
        }
    }

    public class QueryResult
    {
        public int Code { get; set; }
        public string Msg { get; set; }

        public QueryResult()
        {
            Code = -1;
            Msg = "NG";
        }

        public QueryResult(Exception ex)
        {
            Code = -1;
            Msg = ex.Message;
        }

        public QueryResult(int code, string msg)
        {
            Code = code;
            Msg = msg;
        }
    }
}
