using System;
using System.Collections.Generic;
using System.Data;
using SPSZDataLayer.GlobalConfig;

namespace SPSZDataLayer.TableGateway
{
    public class DataUtils
    {
        public static void ResetIndexing(string tableName)
        {
            if(Config.UseSql)
                Sql.SqlUtils.ResetId(tableName);
            
            // TODO: Implement for CSV

        }
    }
}