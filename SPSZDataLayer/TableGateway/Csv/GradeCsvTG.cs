using System;
using System.Collections.Generic;
using System.Data;
using SPSZDataLayer.TableGateway.Interface;

namespace SPSZDataLayer.TableGateway.Csv
{
    public class GradeCsvTG : IGradeTG
    {
        public string TableName = "Grade";
        public int AssignAllInOne(List<DataRow> rows)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow row in rows)
            {
                foreach (DataRow r in table.Rows)
                    if (r["id"].ToString() == row["id"].ToString())
                    {
                        r["student_id"] = row["student_id"];
                        r["subject_id"] = row["subject_id"];
                        r["teacher_id"] = row["teacher_id"];
                        break;
                    }
            }
            CsvUtils.SaveTable(table);
            return 1;
        }

        public int AssignAllInOne(int student_id, int subject_id, int teacher_id, int grade_id)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow r in table.Rows)
                if (r["id"].ToString() == grade_id.ToString())
                {
                    r["student_id"] = student_id;
                    r["subject_id"] = subject_id;
                    r["teacher_id"] = teacher_id;
                    CsvUtils.SaveTable(table);
                    return 1;
                }
            return 0;
        }

        public int AssignGradeToStudent(int student_id, int grade_id)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow r in table.Rows)
                if (r["id"].ToString() == grade_id.ToString())
                {
                    r["student_id"] = student_id;
                    CsvUtils.SaveTable(table);
                    return 1;
                }
            return 0;
        }

        public int AssignGradeToSubject(int subject_id, int grade_id)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow r in table.Rows)
                if (r["id"].ToString() == grade_id.ToString())
                {
                    r["subject_id"] = subject_id;
                    CsvUtils.SaveTable(table);
                    return 1;
                }
            return 0;
        }

        public int AssignGradeToTeacher(int teacher_id, int grade_id)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow r in table.Rows)
                if (r["id"].ToString() == grade_id.ToString())
                {
                    r["teacher_id"] = teacher_id;
                    CsvUtils.SaveTable(table);
                    return 1;
                }
            return 0;
        }

        public void DeleteAll() => CsvUtils.DeleteAll(TableName);

        public int DeleteById(int id) => CsvUtils.DeleteById(id, TableName);

        public List<DataRow> GetAll() => CsvUtils.GetAll(TableName);

        public DataRow GetById(int id) => CsvUtils.GetById(id, TableName);

        public List<DataRow> GetByStudentId(int student_id)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
                if (row["student_id"].ToString() == student_id.ToString())
                    rows.Add(row);
            return rows;

        }

        public List<DataRow> GetByStudentIdAndSubjectId(int student_id, int subject_id)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
                if (row["student_id"].ToString() == student_id.ToString() && row["subject_id"].ToString() == subject_id.ToString())
                    rows.Add(row);
            return rows;
        }

        public int GetStudentID(int grade_id)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow row in table.Rows)
                if (row["id"].ToString() == grade_id.ToString())
                    return Convert.ToInt32(row["student_id"]);
            return -1;
        }

        public int GetSubjectID(int grade_id)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow row in table.Rows)
                if (row["id"].ToString() == grade_id.ToString())
                    return Convert.ToInt32(row["subject_id"]);
            return -1;
        }

        public int GetTeacherID(int grade_id)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow row in table.Rows)
                if (row["id"].ToString() == grade_id.ToString())
                    return Convert.ToInt32(row["teacher_id"]);
            return -1;
        }

        public List<int> Insert(List<DataRow> rows)
        {
            List<int> ids = new List<int>();
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow row in rows)
            {
                int id = CsvUtils.NextRowId(table);
                row["id"] = id;
                table.Rows.Add(row.ItemArray);
                ids.Add(id);
            }
            return ids;
        }

        public int Insert(DataRow row) => CsvUtils.Insert(row, TableName);

        public int Update(DataRow row)
        {
            DataTable table = CsvUtils.LoadTable(TableName);
            foreach (DataRow r in table.Rows)
                if (r["id"].ToString() == row["id"].ToString())
                {
                    r["student_id"] = row["student_id"];
                    r["subject_id"] = row["subject_id"];
                    r["teacher_id"] = row["teacher_id"];
                    r["value"] = row["value"];
                    r["weight"] = row["weight"];
                    r["description"] = row["description"];
                    r["data"] = row["date"];

                    
                    CsvUtils.SaveTable(table);
                    return 1;
                }
            return 0;
        }
    }
}