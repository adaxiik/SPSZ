using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SPSZDomainLayer.Mapper;
using SPSZDomainLayer.Model;

namespace Mapper
{
    public class GradeMapper : IMapper<Grade>
    {
        public static Grade FromRow(DataRow row)
        {
            return new Grade
            {
                Id = row.Table.Columns.Contains("id") ?  Convert.ToInt32(row["id"]) : throw new Exception("Column Id not found"),
                Value = row.Table.Columns.Contains("value") ? Convert.ToInt32(row["value"]) : throw new Exception("Column Value not found"),
                Weight = row.Table.Columns.Contains("weight") ? Convert.ToInt32( row["weight"]) : throw new Exception("Column Weight not found"),
                Description = row.Table.Columns.Contains("description") ?  row["description"] as string : throw new Exception("Column Description not found"),
                Date = row.Table.Columns.Contains("date") ? (DateTime) row["date"] : throw new Exception("Column Date not found"),
            };
        }

        public static List<Grade> FromRows(List<DataRow> rows) => rows.Select(FromRow).ToList();

        public static DataRow ToRow(Grade entity)
        {
            var t = new DataTable();
            t.Columns.Add("id", typeof(int));
            t.Columns.Add("value", typeof(int));
            t.Columns.Add("weight", typeof(int));
            t.Columns.Add("description", typeof(string));
            t.Columns.Add("date", typeof(DateTime));
            var row = t.NewRow();
            row["id"] = entity.Id ?? -1;
            row["value"] = entity.Value;
            row["weight"] = entity.Weight;
            row["description"] = entity.Description ?? String.Empty;
            row["date"] = entity.Date;
            
            return row;
        }

        public static List<DataRow> ToRows(List<Grade> entities) => entities.Select(ToRow).ToList();
    }
}