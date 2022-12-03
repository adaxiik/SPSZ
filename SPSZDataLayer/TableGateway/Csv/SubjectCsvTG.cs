using System;
using System.Collections.Generic;
using System.Data;
using SPSZDataLayer.TableGateway.Interface;

namespace SPSZDataLayer.TableGateway.Csv
{
    public class SubjectCsvTG : ISubjectTG
    {
        public string TableName = "Subject";

        public List<DataRow> GetAll() => CsvUtils.GetAll(TableName);

        public DataRow GetById(int id) => CsvUtils.GetById(id, TableName);

        public int Insert(DataRow row) => CsvUtils.Insert(row, TableName);

        public int Update(DataRow row)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow r in table.Rows)
                if (r["id"].ToString() == row["id"].ToString())
                {
                    r["name"] = row["name"];
                    r["description"] = row["description"];
                    r["label"] = row["label"];
                    CsvUtils.SaveTable(table);
                    return 1;
                }
            return 0;
        }

        public int DeleteById(int id) => CsvUtils.DeleteById(id, TableName);

        public void DeleteAll() => CsvUtils.DeleteAll(TableName);
        
    }
}