using System;
using System.Collections.Generic;
using System.Data;
using SPSZDataLayer.TableGateway.Interface;

namespace SPSZDataLayer.TableGateway.Csv
{
    public class MailboxCsvTG : IMailboxTG
    {
        public string TableName = "Grade";

        public void AssignRecepient(int id, int recepientId)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow r in table.Rows)
            {
                if (r["id"].ToString() == id.ToString())
                {
                    r["recepient_id"] = recepientId;
                    CsvUtils.SaveTable(table);
                    return;
                }
            }
        }

        public void AssignSender(int id, int senderId)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow r in table.Rows)
            {
                if (r["id"].ToString() == id.ToString())
                {
                    r["sender_id"] = senderId;
                    CsvUtils.SaveTable(table);
                    return;
                }
            }
        }

        public void AssignSenderAndRecepient(int id, int senderId, int recepientId)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow r in table.Rows)
            {
                if (r["id"].ToString() == id.ToString())
                {
                    r["sender_id"] = senderId;
                    r["recepient_id"] = recepientId;
                    CsvUtils.SaveTable(table);
                    return;
                }
            }
        }

        public void DeleteAll() => CsvUtils.DeleteAll(TableName);

        public int DeleteById(int id) => CsvUtils.DeleteById(id, TableName);

        public List<DataRow> GetAll() => CsvUtils.GetAll(TableName);

        public DataRow GetById(int id) => CsvUtils.GetById(id, TableName);

        public int Insert(DataRow row) => CsvUtils.Insert(row, TableName);

        public int Update(DataRow row)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow r in table.Rows)
                if (r["id"].ToString() == row["id"].ToString())
                {
                    r["subject"] = row["subject"];
                    r["message"] = row["message"];
                    r["send_date"] = row["send_date"];
                    
                    CsvUtils.SaveTable(table);
                    return 1;
                }
            return 0;
        }
    }
}