using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using SPSZDomainLayer.Model;
namespace SPSZDomainLayer.Mapper
{
    public class TeacherMapper : IMapper<Teacher>
    {
        public static Teacher FromRow(DataRow row)
        {
            return new Teacher
            {
                Id = row.Table.Columns.Contains("id") ? Convert.ToInt32(row["id"]): throw new Exception("Column Id not found"),
                Firstname = row.Table.Columns.Contains("first_name") ? row["first_name"] as string : throw new Exception("Column First Name not found"),
                Lastname = row.Table.Columns.Contains("last_name") ? row["last_name"] as string : throw new Exception("Column Last Name not found"),
                Email = row.Table.Columns.Contains("email") ? row["email"] as string : throw new Exception("Column Email not found"),
                Password = row.Table.Columns.Contains("password") ? row["password"] as string : throw new Exception("Column Password not found"),
            };
        }
        public static List<Teacher> FromRows(List<DataRow> rows) => rows.Select(FromRow).ToList();
        public static DataRow ToRow(Teacher entity)
        {
            var t = PersonMapper.GetPersonTable();


            var row = t.NewRow();
            row["id"] = entity.Id ?? -1;
            row["first_name"] = entity.Firstname ?? String.Empty;
            row["last_name"] = entity.Lastname ?? String.Empty;
            row["email"] = entity.Email ?? String.Empty;
            row["password"] = entity.Password ?? String.Empty;
            row["type"] = "teacher";
            return row;
        }
        public static List<DataRow> ToRows(List<Teacher> entities) => entities.Select(ToRow).ToList();
    }


}