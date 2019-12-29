using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebShopManagement.DataAccess
{
    public class DatabaseAccess
    {
        SqlConnection con;
        public DatabaseAccess()
        {
            var configuration = GetConfiguration();
            con = new SqlConnection(configuration.GetSection("Data").GetSection("NhucConnectionString").Value);
        }
        public static IConfigurationRoot GetConfiguration()
        {
            var builer = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builer.Build();
        }
        
        public string ExecuteProcedureReturnString(string connString, string procName, params SqlParameter[] paramters)
        {
            string result = "";
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = procName;
                    if (paramters != null)
                    {
                        command.Parameters.AddRange(paramters);
                    }
                    sqlConnection.Open();
                    var ret = command.ExecuteScalar();
                    if (ret != null)
                        result = Convert.ToString(ret);
                }
            }
            return result;
        }

        public TData ExtecuteProcedureReturnData<TData>(string connString, string procName,Func<SqlDataReader, TData> translator, params SqlParameter[] parameters)
        {
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = procName;
                    if (parameters != null)
                    {
                        sqlCommand.Parameters.AddRange(parameters);
                    }
                    sqlConnection.Open();
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        TData elements;
                        try
                        {
                            elements = translator(reader);
                        }
                        finally
                        {
                            while (reader.NextResult())
                            { }
                        }
                        return elements;
                    }
                }
            }
        }
        #region Get Values from Sql Data Reader  
        //public string GetNullableString(SqlDataReader reader, string colName)
        //{
        //    return reader.IsDBNull(reader.GetOrdinal(colName)) ? null : Convert.ToString(reader[colName]);
        //}

        //public int GetNullableInt32(SqlDataReader reader, string colName)
        //{
        //    return reader.IsDBNull(reader.GetOrdinal(colName)) ? 0 : Convert.ToInt32(reader[colName]);
        //}

        //public bool GetBoolean(SqlDataReader reader, string colName)
        //{
        //    return reader.IsDBNull(reader.GetOrdinal(colName)) ? default(bool) : Convert.ToBoolean(reader[colName]);
        //}

        ////this method is to check wheater column exists or not in data reader  
        //public bool IsColumnExists(this System.Data.IDataRecord dr, string colName)
        //{
        //    try
        //    {
        //        return (dr.GetOrdinal(colName) >= 0);
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        #endregion
    }
}
