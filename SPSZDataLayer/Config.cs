using System.Data.Common;
using System;

namespace SPSZDataLayer.GlobalConfig
{
    public class Config
    {
        public static IDataConnection Connection { get; private set; } = new SqlConnection();
        public static readonly string SQLConnectionString = "Data Source=SPSZdatabase.db;";
    }
}