using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using SPSZDomainLayer.Model;

namespace SPSZDomainLayer.Mapper
{
    public class StudentMapper : IMapper<Student>
    {
        public static Student FromRow(DataRow row)
        {
            return new Student
            {
                Id = row.Table.Columns.Contains("id") ?  row["id"] as int? : throw new Exception("Column Id not found"),
                Firstname = row.Table.Columns.Contains("first_name") ?  row["first_name"] as string : throw new Exception("Column First Name not found"),
                Lastname = row.Table.Columns.Contains("last_name") ?  row["last_name"] as string : throw new Exception("Column Last Name not found"),
                Address = row.Table.Columns.Contains("address") ?  row["address"] as string : throw new Exception("Column Address not found"),
            };
        }

        public static List<Student> FromRows(List<DataRow> rows) => rows.Select(FromRow).ToList();

        public static DataRow ToRow(Student entity)
        {
            var t = PersonMapper.GetPersonTable();

            var row = t.NewRow();
            row["id"] = entity.Id ?? -1;
            row["first_name"] = entity.Firstname ?? String.Empty;
            row["last_name"] = entity.Lastname ?? String.Empty;
            row["address"] = entity.Address ?? String.Empty;
            row["type"] = "student";
            return row;
        }

        public static List<DataRow> ToRows(List<Student> entities) => entities.Select(ToRow).ToList();
    }
}