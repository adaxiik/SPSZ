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

        public static string ToMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}