using System.IO;
using System.Data.Common;
using System;

namespace SPSZDataLayer.GlobalConfig
{
    public class Config
    {
        private static IDataConnection _connection;
        public static readonly bool UseSql = true; 
        public static IDataConnection Connection {
            get {
                if (UseSql)
                {
                    return _connection ??= new SqlConnection();
                }
                else
                {
                    return _connection ??= new CsvConnection();
                }
            }
        }
        public static readonly string SQLConnectionString = "Data Source=SPSZdatabase.db;";
        // set CsvDBFolder to CsvDatabases/
        public static readonly string CsvDBFolder = "CsvDatabases";
        public static readonly string CsvDelimiter = ";";

    }
}