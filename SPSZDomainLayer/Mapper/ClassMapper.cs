using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SPSZDomainLayer.Mapper;
using SPSZDomainLayer.Model;

namespace Mapper
{
    public class ClassMapper : IMapper<ClassRoom>
    {
        public static ClassRoom FromRow(DataRow row)
        {
            return new ClassRoom
            {
                Id = row.Table.Columns.Contains("id") ?  row["id"] as int? : throw new Exception("Column Id not found"),
                ClassName = row.Table.Columns.Contains("class_name") ?  row["class_name"] as string : throw new Exception("Column ClassName not found"),
            };
        }

        public static List<ClassRoom> FromRows(List<DataRow> rows) => rows.Select(FromRow).ToList();

        public static DataRow ToRow(ClassRoom entity)
        {
            DataTable t = new DataTable();
            t.Columns.Add("id", typeof(int));
            t.Columns.Add("class_name", typeof(string));
            DataRow row = t.NewRow();
            row["id"] = entity.Id ?? -1;
            row["class_name"] = entity.ClassName ?? String.Empty;
            
            return row;
        }

        public static List<DataRow> ToRows(List<ClassRoom> entities) => entities.Select(ToRow).ToList();
    }
}