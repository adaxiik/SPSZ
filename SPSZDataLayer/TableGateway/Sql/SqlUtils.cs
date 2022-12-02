using System.Collections.Generic;
using System.Data;
using System;
using Microsoft.Data.Sqlite;
using SPSZDataLayer.GlobalConfig;

namespace SPSZDataLayer.TableGateway.Sql
{
    public class SqlUtils
    {
        public static DataRow GetSingleRowOrNullOrError(DataTable table)
        {
            if (table.Rows.Count > 1)
                throw new Exception("More than one row returned");
            else if (table.Rows.Count == 0)
                return null;
            else
                return table.Rows[0];
        }

        public static DataTable MakeQuery(string query, List<SqliteParameter> parameters = null)
        {
            using (var connection = new SqliteConnection(Config.SQLConnectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    if (parameters is not null)
                        foreach (var parameter in parameters)
                            command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        var table = new DataTable();
                        table.Load(reader);
                        return table;
                    }
                }
            }
        }

        public static int MakeNonQuery(string query, List<SqliteParameter> parameters = null)
        {
            using (var connection = new SqliteConnection(Config.SQLConnectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    if (parameters is not null)
                        foreach (var parameter in parameters)
                            command.Parameters.Add(parameter);

                    return command.ExecuteNonQuery();
                }
            }
        }

        public static int GetLastId()
        {
            using (var connection = new SqliteConnection(Config.SQLConnectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("SELECT last_insert_rowid()", connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public static void ResetId(string tableName)
        {
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@tableName", tableName)
            };
            MakeNonQuery("DELETE FROM sqlite_sequence WHERE name = @tableName", parameters);
        }
    }
}