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
                Id = row.Table.Columns.Contains("id") ? Convert.ToInt32(row["id"])  : throw new Exception("Column Id not found"),
                Firstname = row.Table.Columns.Contains("first_name") ? (string) row["first_name"]  : throw new Exception("Column First Name not found"),
                Lastname = row.Table.Columns.Contains("last_name") ? (string) row["last_name"]  : throw new Exception("Column Last Name not found"),
                Address = row.Table.Columns.Contains("address") ? (string) row["address"]  : throw new Exception("Column Address not found"),
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