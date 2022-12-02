using System;
using System.Data;
using SPSZDataLayer.TableGateway.Interface;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace SPSZDataLayer.TableGateway.Sql
{
    public class StudentSqlTG : IStudentTG
    {
        readonly string TableName = "Person";

        public List<DataRow> GetAll()
        {
            string query = $"SELECT id, first_name, last_name, address FROM {TableName} WHERE type = 'student'";
            var t = SqlUtils.MakeQuery(query);
            return t.Rows.Cast<DataRow>().ToList();
        }

        public DataRow GetById(int id)
        {
            string query = $"SELECT id, first_name, last_name, address FROM {TableName} WHERE id = @id AND type = 'student'";
            List<SqliteParameter> parameters = new List<SqliteParameter>();
            parameters.Add(new SqliteParameter("@id", id));
            var t = SqlUtils.MakeQuery(query, parameters);
            return SqlUtils.GetSingleRowOrNullOrError(t);
        }

        public int Insert(DataRow row)
        {
            string query = $"INSERT INTO {TableName} (first_name, last_name, address, type) VALUES (@first_name, @last_name, @address, 'student')";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@first_name", row["first_name"]),
                new SqliteParameter("@last_name", row["last_name"]),
                new SqliteParameter("@address", row["address"])
            };
            SqlUtils.MakeNonQuery(query, parameters);
            return SqlUtils.GetLastId();
        }

        public int Update(DataRow row)
        {
            string query = $"UPDATE {TableName} SET first_name = @first_name, last_name = @last_name, address = @address WHERE id = @id AND type = 'student'";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@first_name", row["first_name"]),
                new SqliteParameter("@last_name", row["last_name"]),
                new SqliteParameter("@address", row["address"]),
                new SqliteParameter("@id", row["id"])
            };
            return SqlUtils.MakeNonQuery(query, parameters);
        }

        public int DeleteById(int id)
        {
            string query = $"DELETE FROM {TableName} WHERE id = @id AND type = 'student'";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@id", id)
            };
            return SqlUtils.MakeNonQuery(query, parameters);
        }

        public void DeleteAll()
        {
            string query = $"DELETE FROM {TableName} WHERE type = 'student'";
            SqlUtils.MakeNonQuery(query);
        }

        public int SetParentId(int id, int parent_id)
        {
            string query = $"UPDATE {TableName} SET parent_id = @parent_id WHERE id = @id AND type = 'student'";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@parent_id", parent_id),
                new SqliteParameter("@id", id)
            };
            return SqlUtils.MakeNonQuery(query, parameters);
        }

        public int SetClassId(int id, int class_id)
        {
            string query = $"UPDATE {TableName} SET class_id = @class_id WHERE id = @id AND type = 'student'";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@class_id", class_id),
                new SqliteParameter("@id", id)
            };
            return SqlUtils.MakeNonQuery(query, parameters);
        }
    }
}