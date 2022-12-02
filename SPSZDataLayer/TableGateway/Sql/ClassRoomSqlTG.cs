using System;
using System.Data;
using SPSZDataLayer.TableGateway.Interface;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace SPSZDataLayer.TableGateway.Sql
{
    public class ClassRoomSqlTG : IClassRoomTG
    {
        readonly string TableName = "ClassRoom";

        public List<DataRow> GetAll()
        {
            string query = $"SELECT id, name FROM {TableName}";
            var t = SqlUtils.MakeQuery(query);
            return t.Rows.Cast<DataRow>().ToList();
        }

        public DataRow GetById(int id)
        {
            string query = $"SELECT id, name FROM {TableName} WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@id", id)
            };
            var t = SqlUtils.MakeQuery(query, parameters);
            return SqlUtils.GetSingleRowOrNullOrError(t);
        }

        public int Insert(DataRow row)
        {
            string query = $"INSERT INTO {TableName} (name) VALUES (@name)";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@name", row["name"])
            };
            SqlUtils.MakeNonQuery(query, parameters);
            return SqlUtils.GetLastId();
        }

        public int Update(DataRow row)
        {
            string query = $"UPDATE {TableName} SET name = @name WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@name", row["name"]),
                new SqliteParameter("@id", row["id"])
            };
            return SqlUtils.MakeNonQuery(query, parameters);
        }

        public int DeleteById(int id)
        {
            string query = $"DELETE FROM {TableName} WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@id", id)
            };
            return SqlUtils.MakeNonQuery(query, parameters);
        }

        public void DeleteAll()
        {
            string query = $"DELETE FROM {TableName}";
            SqlUtils.MakeNonQuery(query);
            SqlUtils.ResetId(TableName);
        }
    }
}