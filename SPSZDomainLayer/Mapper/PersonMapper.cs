using System.Data;
using System;

namespace SPSZDomainLayer.Mapper
{
    public class PersonMapper
    {
        public static DataTable GetPersonTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("first_name", typeof(string));
            table.Columns.Add("last_name", typeof(string));
            table.Columns.Add("type", typeof(string));

            // that can be null:
            table.Columns.Add("address", typeof(string));
            table.Columns.Add("email", typeof(string));
            table.Columns.Add("password", typeof(string));

            table.Columns["address"].AllowDBNull = true;
            table.Columns["email"].AllowDBNull = true;
            table.Columns["password"].AllowDBNull = true;
            return table;
        }
    }
}