using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SPSZDomainLayer.Model;

namespace SPSZDomainLayer.Mapper
{
    public class SubjectMapper : IMapper<Subject>
    {
        public static Subject FromRow(DataRow row)
        {
            return new Subject
            {
                Id = row.Table.Columns.Contains("id") ?  Convert.ToInt32(row["id"]): throw new Exception("Column Id not found"),
                Name = row.Table.Columns.Contains("name") ?  row["name"] as string : throw new Exception("Column Name not found"),
                Description = row.Table.Columns.Contains("description") ?  row["description"] as string : throw new Exception("Column Description not found"),
                Label = row.Table.Columns.Contains("label") ?  row["label"] as string : throw new Exception("Column Label not found"),
            };
        }

        public static List<Subject> FromRows(List<DataRow> rows) => rows.Select(FromRow).ToList();

        public static DataRow ToRow(Subject entity)
        {
            var t = new DataTable();
            t.Columns.Add("id", typeof(int));
            t.Columns.Add("name", typeof(string));
            t.Columns.Add("description", typeof(string));
            t.Columns.Add("label", typeof(string));
            var row = t.NewRow();
            row["id"] = entity.Id ?? -1;
            row["name"] = entity.Name ?? String.Empty;
            row["description"] = entity.Description ?? String.Empty;
            row["label"] = entity.Label ?? String.Empty;
            return row;
        }

        public static List<DataRow> ToRows(List<Subject> entities) => entities.Select(ToRow).ToList();
    }

}