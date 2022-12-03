using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SPSZDomainLayer.Mapper;
using SPSZDomainLayer.Model;

namespace SPSZDomainLayer.Mapper
{
    public class MailMapper : IMapper<Grade>
    {
        public static Mail FromRow(DataRow row)
        {
            return new Mail
            {
                Id = row.Table.Columns.Contains("id") ?  Convert.ToInt32(row["id"]) : throw new Exception("Column Id not found"),
                Date = row.Table.Columns.Contains("send_date") ? DateTime.Parse(row["send_date"].ToString())  : throw new Exception("Column Date not found"),
                Subject = row.Table.Columns.Contains("subject") ?  row["subject"] as string : throw new Exception("Column Subject not found"),
                Message = row.Table.Columns.Contains("message") ?  row["message"] as string : throw new Exception("Column Message not found")
            };
        }

        public static List<Mail> FromRows(List<DataRow> rows) => rows.Select(FromRow).ToList();

        public static DataRow ToRow(Mail entity)
        {
            var t = new DataTable();
            t.Columns.Add("id", typeof(int));
            t.Columns.Add("send_date", typeof(DateTime));
            t.Columns.Add("subject", typeof(string));
            t.Columns.Add("message", typeof(string));
            
            var row = t.NewRow();
            row["id"] = entity.Id ?? -1;
            row["send_date"] = entity.Date;
            row["subject"] = entity.Subject ?? String.Empty;
            row["message"] = entity.Message ?? String.Empty;
            
            return row;
        }

        public static List<DataRow> ToRows(List<Mail> entities) => entities.Select(ToRow).ToList();
    }
}