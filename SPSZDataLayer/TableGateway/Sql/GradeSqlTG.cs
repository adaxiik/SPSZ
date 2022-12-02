using System;
using System.Data;
using SPSZDataLayer.TableGateway.Interface;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace SPSZDataLayer.TableGateway.Sql
{
    public class GradeSqlTG : IGradeTG
    {
        readonly string TableName = "Grade";

        public List<DataRow> GetAll()
        {
            string query = $"SELECT id, value, weight, description, date FROM {TableName}";
            var t = SqlUtils.MakeQuery(query);
            return t.Rows.Cast<DataRow>().ToList();
        }

        public DataRow GetById(int id)
        {
            string query = $"SELECT id, value, weight, description, date FROM {TableName} WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>();
            parameters.Add(new SqliteParameter("@id", id));
            var t = SqlUtils.MakeQuery(query, parameters);
            return SqlUtils.GetSingleRowOrNullOrError(t);
        }

        public int Insert(DataRow row)
        {
            string query = $"INSERT INTO {TableName} (value, weight, description, date) VALUES (@value, @weight, @description, @date)";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@value", row["value"]),
                new SqliteParameter("@weight", row["weight"]),
                new SqliteParameter("@description", row["description"]),
                new SqliteParameter("@date", row["date"])
            };
            SqlUtils.MakeNonQuery(query, parameters);
            return SqlUtils.GetLastId();
        }

        public int Update(DataRow row)
        {
            string query = $"UPDATE {TableName} SET value = @value, weight = @weight, description = @description, date = @date WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@value", row["value"]),
                new SqliteParameter("@weight", row["weight"]),
                new SqliteParameter("@description", row["description"]),
                new SqliteParameter("@date", row["date"]),
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