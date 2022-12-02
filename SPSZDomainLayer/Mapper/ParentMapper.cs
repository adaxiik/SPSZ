using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SPSZDomainLayer.Model;

namespace SPSZDomainLayer.Mapper
{
    public class ParentMapper : IMapper<Parent>
    {
        public static Parent FromRow(DataRow row)
        {
            Parent parent = new Parent()
            {
                Id = row.Table.Columns.Contains("id") ? Convert.ToInt32(row["id"]) : throw new Exception("Column Id not found"),
                Firstname = row.Table.Columns.Contains("first_name") ? row["first_name"] as string : throw new Exception("Column First Name not found"),
                Lastname = row.Table.Columns.Contains("last_name") ? row["last_name"] as string : throw new Exception("Column Last Name not found"),
                Email = row.Table.Columns.Contains("email") ? row["email"] as string : throw new Exception("Column Email not found"),
                Password = row.Table.Columns.Contains("password") ? row["password"] as string : throw new Exception("Column Password not found"),
            };      
            return parent;
        }

        public static List<Parent> FromRows(List<DataRow> rows) => rows.Select(FromRow).ToList();

        public static DataRow ToRow(Parent parent)
        {
            DataTable table = PersonMapper.GetPersonTable();

            DataRow row = table.NewRow();
            row["id"] = parent.Id ?? -1;
            row["first_name"] = parent.Firstname ?? String.Empty;
            row["last_name"] = parent.Lastname ?? String.Empty;
            row["email"] = parent.Email ?? String.Empty;
            row["password"] = parent.Password ?? String.Empty;
            row["type"] = "parent";
            return row;
        }

        public static List<DataRow> ToRows(List<Parent> parents) => parents.Select(ToRow).ToList();

 
    }
}