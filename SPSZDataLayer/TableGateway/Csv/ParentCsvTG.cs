using System;
using System.Collections.Generic;
using System.Data;
using SPSZDataLayer.TableGateway.Interface;

namespace SPSZDataLayer.TableGateway.Csv
{
    public class ParentCsvTG : IParentTG
    {
        public string TableName = "Person";
        public void DeleteAll() => CsvUtils.DeleteAllByType(TableName, "parent");

        public int DeleteById(int id) => CsvUtils.DeleteByIdAndType(id, TableName, "parent");

        public List<DataRow> GetAll() => CsvUtils.GetAllByType(TableName, "parent");

        public DataRow GetById(int id) => CsvUtils.GetByIdAndType(id, TableName, "parent");

        public List<DataRow> GetByLastname(string lastName)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
                if (row["last_name"].ToString() == lastName)
                    rows.Add(row);
            return rows;
        }

        public int Insert(DataRow row) => CsvUtils.Insert(row, TableName);
        public int Update(DataRow row)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow r in table.Rows)
                if (r["id"].ToString() == row["id"].ToString())
                {
                    r["first_name"] = row["first_name"];
                    r["last_name"] = row["last_name"];
                    r["email"] = row["email"];
                    r["password"] = row["password"];
                    CsvUtils.SaveTable(table);
                    return 1;
                }
            return 0;
        }
    }
}