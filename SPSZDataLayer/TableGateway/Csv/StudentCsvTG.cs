using System;
using System.Collections.Generic;
using System.Data;
using SPSZDataLayer.TableGateway.Interface;

namespace SPSZDataLayer.TableGateway.Csv
{
    public class StudentCsvTG : IStudentTG
    {
        public string TableName = "Person";
        public void DeleteAll() => CsvUtils.DeleteAllByType(TableName, "student");

        public int DeleteById(int id) => CsvUtils.DeleteByIdAndType(id, TableName, "student");

        public List<DataRow> GetAll() => CsvUtils.GetAllByType(TableName, "student");

        public DataRow GetById(int id) => CsvUtils.GetByIdAndType(id, TableName, "student");

        public int GetStudentClassId(int id)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow row in table.Rows)
                if (row["id"].ToString() == id.ToString())
                    return Convert.ToInt32(row["class_id"]);
            return -1;
        }

        public List<DataRow> GetStudentsByClassId(int class_id)
        {
            List<DataRow> students = new List<DataRow>();
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow row in table.Rows)
                if (row["class_id"].ToString() == class_id.ToString())
                    students.Add(row);
            return students;
        }

        public List<DataRow> GetStudentsByParentId(int parent_id)
        {
            List<DataRow> students = new List<DataRow>();
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow row in table.Rows)
                if (row["parent_id"].ToString() == parent_id.ToString())
                    students.Add(row);
            return students;
        }

        public int Insert(DataRow row) => CsvUtils.Insert(row, TableName);

        public int SetClassId(int id, int class_id)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow row in table.Rows)
                if (row["id"].ToString() == id.ToString())
                {
                    row["class_id"] = class_id;
                    CsvUtils.SaveTable(table);
                    return 1;
                }
            return 0;
        }

        public int SetParentId(int id, int parent_id)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow row in table.Rows)
                if (row["id"].ToString() == id.ToString())
                {
                    row["parent_id"] = parent_id;
                    CsvUtils.SaveTable(table);
                    return 1;
                }
            return 0;
        }

        public int Update(DataRow row)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow r in table.Rows)
                if (r["id"].ToString() == row["id"].ToString())
                {
                    r["first_name"] = row["first_name"];
                    r["last_name"] = row["last_name"];
                    r["class_id"] = row["class_id"];
                    r["parent_id"] = row["parent_id"];
                    r["address"] = row["address"];
                    CsvUtils.SaveTable(table);
                    return 1;
                }
            return 0;
        }
    }
}