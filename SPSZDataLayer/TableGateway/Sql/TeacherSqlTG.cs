using System;
using System.Data;
using SPSZDataLayer.TableGateway.Interface;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace SPSZDataLayer.TableGateway.Sql
{
    public class TeacherSqlTG : ITeacherTG
    {
        readonly string TableName = "Person";

        public List<DataRow> GetAll()
        {
            string query = $"SELECT id, first_name, last_name, email, password FROM {TableName} WHERE type = 'teacher'";
            var t = SqlUtils.MakeQuery(query);
            return t.Rows.Cast<DataRow>().ToList();
        }

        public DataRow GetById(int id)
        {
            string query = $"SELECT id, first_name, last_name, email, password FROM {TableName} WHERE id = @id AND type = 'teacher'";
            List<SqliteParameter> parameters = new List<SqliteParameter>();
            parameters.Add(new SqliteParameter("@id", id));
            var t = SqlUtils.MakeQuery(query, parameters);
            return SqlUtils.GetSingleRowOrNullOrError(t);
        }

        public int Insert(DataRow row)
        {
            string query = $"INSERT INTO {TableName} (first_name, last_name, email, password, type) VALUES (@first_name, @last_name, @email, @password, 'teacher')";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@first_name", row["first_name"]),
                new SqliteParameter("@last_name", row["last_name"]),
                new SqliteParameter("@email", row["email"]),
                new SqliteParameter("@password", row["password"])
            };
            SqlUtils.MakeNonQuery(query, parameters);
            return SqlUtils.GetLastId();
        }

        public int Update(DataRow row)
        {
            string query = $"UPDATE {TableName} SET first_name = @first_name, last_name = @last_name, email = @email, password = @password WHERE id = @id AND type = 'teacher'";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@first_name", row["first_name"]),
                new SqliteParameter("@last_name", row["last_name"]),
                new SqliteParameter("@email", row["email"]),
                new SqliteParameter("@password", row["password"]),
                new SqliteParameter("@id", row["id"])
            };
            return SqlUtils.MakeNonQuery(query, parameters);
        }

        public int DeleteById(int id)
        {
            string query = $"DELETE FROM {TableName} WHERE id = @id AND type = 'teacher'";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@id", id)
            };
            return SqlUtils.MakeNonQuery(query, parameters);
        }

        public void DeleteAll()
        {
            string query = $"DELETE FROM {TableName} WHERE type = 'teacher'";
            SqlUtils.MakeNonQuery(query);
        }
    }
}