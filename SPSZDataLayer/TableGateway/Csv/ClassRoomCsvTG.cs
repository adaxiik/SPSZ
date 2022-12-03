using System;
using System.Collections.Generic;
using System.Data;
using SPSZDataLayer.TableGateway.Interface;

namespace SPSZDataLayer.TableGateway.Csv
{
    public class ClassRoomCsvTG : IClassRoomTG
    {
        public string TableName = "ClassRoom";
        public int AssignClassRoomToTeacher(int teacherId, int classRoomId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll() => CsvUtils.DeleteAll(TableName);

        public int DeleteById(int id) => CsvUtils.DeleteById(id, TableName);

        public List<DataRow> GetAll() => CsvUtils.GetAll(TableName);

        public DataRow GetById(int id) => CsvUtils.GetById(id, TableName);

        public List<DataRow> GetClassRoomsByTeacherId(int teacherId)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
                if (row["teacher_id"].ToString() == teacherId.ToString())
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
                    r["class_name"] = row["class_name"];
                    //r["teacher_id"] = row["teacher_id"];
                    CsvUtils.SaveTable(table);
                    return 1;
                }
            return 0;
        }
    }
}