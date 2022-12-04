using System.Runtime.CompilerServices;
using System;
using System.Data;
using SPSZDataLayer.TableGateway.Interface;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using SPSZDataLayer.GlobalConfig;

namespace SPSZDataLayer.TableGateway.Sql
{
    public class MailboxSqlTG : IMailboxTG
    {
        readonly string TableName = "Mailbox";

        

        public void DeleteAll()
        {
            string query = $"DELETE FROM {TableName}";
            SqlUtils.MakeNonQuery(query);
            SqlUtils.ResetId(TableName);
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


        public List<DataRow> GetAll()
        {
            string query = $"SELECT id, subject, message, send_date  FROM {TableName}";
            var t = SqlUtils.MakeQuery(query);
            return t.Rows.Cast<DataRow>().ToList();
        }

        public DataRow GetById(int id)
        {
            string query = $"SELECT id, subject, message, send_date  FROM {TableName} WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@id", id)
            };
            var t = SqlUtils.MakeQuery(query, parameters);
            return SqlUtils.GetSingleRowOrNullOrError(t);
        }

        public int Insert(DataRow row)
        {
            string query = $"INSERT INTO {TableName} (subject, message, send_date) VALUES (@subject, @message, @send_date)";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@subject", row["subject"]),
                new SqliteParameter("@message", row["message"]),
                new SqliteParameter("@send_date", row["send_date"])
            };
            SqlUtils.MakeNonQuery(query, parameters);
            return SqlUtils.GetLastId();
        }

        public int Update(DataRow row)
        {
            string query = $"UPDATE {TableName} SET subject = @subject, message = @message, send_date = @send_date WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@id", row["id"]),
                new SqliteParameter("@subject", row["subject"]),
                new SqliteParameter("@message", row["message"]),
                new SqliteParameter("@send_date", row["send_date"])
            };
            return SqlUtils.MakeNonQuery(query, parameters);
        }

        public void AssignRecepient(int id, int recepientId)
        {
            string query = $"UPDATE {TableName} SET recepient_id = @recepient_id WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@id", id),
                new SqliteParameter("@recepient_id", recepientId)
            };
            SqlUtils.MakeNonQuery(query, parameters);

        }

        public void AssignSender(int id, int senderId)
        {
            string query = $"UPDATE {TableName} SET sender_id = @sender_id WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@id", id),
                new SqliteParameter("@sender_id", senderId)
            };
            SqlUtils.MakeNonQuery(query, parameters);
        }

        public void AssignSenderAndRecepient(int id, int senderId, int recepientId)
        {
            string query = $"UPDATE {TableName} SET sender_id = @sender_id, recepient_id = @recepient_id WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@id", id),
                new SqliteParameter("@sender_id", senderId),
                new SqliteParameter("@recepient_id", recepientId)
            };
            SqlUtils.MakeNonQuery(query, parameters);
        }

        public List<DataRow> GetAllMailsOfRecepient(int recepientId)
        {
            string query = $"SELECT id, subject, message, send_date  FROM {TableName} WHERE recepient_id = @recepient_id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@recepient_id", recepientId)
            };
            var t = SqlUtils.MakeQuery(query, parameters);
            return t.Rows.Cast<DataRow>().ToList();
        }

        public int GetSenderId(int id)
        {
            string query = $"SELECT sender_id FROM {TableName} WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@id", id)
            };
            var t = SqlUtils.MakeQuery(query, parameters);
            var row = SqlUtils.GetSingleRowOrNullOrError(t);
            return Convert.ToInt32(row["sender_id"]);
        }
    }
}