using System;
using System.Collections.Generic;
using System.Data;
using SPSZDataLayer.TableGateway.Interface;

namespace SPSZDataLayer.TableGateway.Csv
{
    public class TeacherCsvTG : ITeacherTG
    {
        public string TableName = "Person";
        public void DeleteAll() => CsvUtils.DeleteAllByType(TableName, "teacher");

        public int DeleteById(int id) => CsvUtils.DeleteByIdAndType(id, TableName, "teacher");

        public List<DataRow> GetAll() => CsvUtils.GetAllByType(TableName, "teacher");

        public DataRow GetById(int id) => CsvUtils.GetByIdAndType(id, TableName, "teacher");

        public int Insert(DataRow row) => CsvUtils.Insert(row, TableName);

        public int Update(DataRow row)
        {
            
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow r in table.Rows)
                if (r["id"].ToString() == row["id"].ToString())
                {
                    r["fist_name"] = row["fist_name"];
                    r["last name"] = row["last name"];
                    r["email"] = row["email"];
                    r["password"] = row["password"];
                    CsvUtils.SaveTable(table);
                    return 1;
                }
            return 0;
        }
    }
}