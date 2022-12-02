using System;
using System.Data;
using SPSZDataLayer.TableGateway.Interface;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace SPSZDataLayer.TableGateway.Sql
{
    public class SubjectSqlTG : ISubjectTG
    {
        readonly string TableName = "Subject";

        public List<DataRow> GetAll()
        {
            string query = $"SELECT id, name, description, label FROM {TableName}";
            var t = SqlUtils.MakeQuery(query);
            return t.Rows.Cast<DataRow>().ToList();
        }

        public DataRow GetById(int id)
        {
            string query = $"SELECT id, name, description, label FROM {TableName} WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>();
            parameters.Add(new SqliteParameter("@id", id));
            var t = SqlUtils.MakeQuery(query, parameters);
            return SqlUtils.GetSingleRowOrNullOrError(t);
        }

        public int Insert(DataRow row)
        {
            string query = $"INSERT INTO {TableName} (name, description, label) VALUES (@name, @description, @label)";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@name", row["name"]),
                new SqliteParameter("@description", row["description"]),
                new SqliteParameter("@label", row["label"])
            };
            SqlUtils.MakeNonQuery(query, parameters);
            return SqlUtils.GetLastId();
        }

        public int Update(DataRow row)
        {
            string query = $"UPDATE {TableName} SET name = @name, description = @description, label = @label WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@name", row["name"]),
                new SqliteParameter("@description", row["description"]),
                new SqliteParameter("@label", row["label"]),
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
            string query = "DELETE FROM Subject";
            SqlUtils.MakeNonQuery(query);
            SqlUtils.ResetId("Subject");
        }
    }
}