using System.Runtime.CompilerServices;
using System;
using System.Data;
using SPSZDataLayer.TableGateway.Interface;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using SPSZDataLayer.GlobalConfig;

namespace SPSZDataLayer.TableGateway.Sql
{
    public class GradeSqlTG : IGradeTG
    {
        readonly string TableName = "Grade";

        public List<DataRow> GetAll()
        {
            string query = $"SELECT id, value, weight, description, date FROM {TableName}";
            var t = SqlUtils.MakeQuery(query);
            return t.Rows.Cast<DataRow>().ToList();
        }

        public DataRow GetById(int id)
        {
            string query = $"SELECT id, value, weight, description, date FROM {TableName} WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>();
            parameters.Add(new SqliteParameter("@id", id));
            var t = SqlUtils.MakeQuery(query, parameters);
            return SqlUtils.GetSingleRowOrNullOrError(t);
        }

        public int Insert(DataRow row)
        {
            string query = $"INSERT INTO {TableName} (value, weight, description, date) VALUES (@value, @weight, @description, @date)";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@value", row["value"]),
                new SqliteParameter("@weight", row["weight"]),
                new SqliteParameter("@description", row["description"]),
                new SqliteParameter("@date", row["date"])
            };
            SqlUtils.MakeNonQuery(query, parameters);
            return SqlUtils.GetLastId();
        }

        public int Update(DataRow row)
        {
            string query = $"UPDATE {TableName} SET value = @value, weight = @weight, description = @description, date = @date WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@value", row["value"]),
                new SqliteParameter("@weight", row["weight"]),
                new SqliteParameter("@description", row["description"]),
                new SqliteParameter("@date", row["date"]),
                new SqliteParameter("@id", row["id"])
            };
            return SqlUtils.MakeNonQuery(query, parameters);
        }

        public int DeleteById(int id)
        {
            string query = $"DELETE FROM {TableName} WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@id", id)
            };
            return SqlUtils.MakeNonQuery(query, parameters);
        }

        public void DeleteAll()
        {
            string query = $"DELETE FROM {TableName}";
            SqlUtils.MakeNonQuery(query);
            SqlUtils.ResetId(TableName);
        }

        public int AssignGradeToStudent(int student_id, int grade_id)
        {
            string query = $"UPDATE Grade SET student_id = @student_id WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@student_id", student_id),
                new SqliteParameter("@id", grade_id)
            };
            return SqlUtils.MakeNonQuery(query, parameters);
        }

        public int AssignGradeToSubject(int subject_id, int grade_id)
        {
            string query = $"UPDATE Grade SET subject_id = @subject_id WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@subject_id", subject_id),
                new SqliteParameter("@id", grade_id)
            };
            return SqlUtils.MakeNonQuery(query, parameters);
        }

        public int AssignGradeToTeacher(int teacher_id, int grade_id)
        {
            string query = $"UPDATE Grade SET teacher_id = @teacher_id WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@teacher_id", teacher_id),
                new SqliteParameter("@id", grade_id)
            };
            return SqlUtils.MakeNonQuery(query, parameters);
        }

        public int AssignAllInOne(int student_id, int subject_id, int teacher_id, int grade_id)
        {
            return  AssignGradeToStudent(student_id, grade_id) +
                    AssignGradeToSubject(subject_id, grade_id) +
                    AssignGradeToTeacher(teacher_id, grade_id);
        }

        public List<int> Insert(List<DataRow> rows)
        {
            string query = $"INSERT INTO {TableName} (value, weight, description, date) VALUES (@value, @weight, @description, @date)";
            List<int> ids = new List<int>();
            using(var connection = new SqliteConnection(Config.SQLConnectionString))
            {
                connection.Open();
                using(var transaction = connection.BeginTransaction())
                {
                    using(var command = new SqliteCommand(query, connection, transaction))
                    {
                        command.Parameters.Add("@value", SqliteType.Integer);
                        command.Parameters.Add("@weight", SqliteType.Integer);
                        command.Parameters.Add("@description", SqliteType.Text);
                        command.Parameters.Add("@date", SqliteType.Text);
                        foreach(var row in rows)
                        {
                            command.Parameters["@value"].Value = row["value"];
                            command.Parameters["@weight"].Value = row["weight"];
                            command.Parameters["@description"].Value = row["description"];
                            command.Parameters["@date"].Value = row["date"];
                            command.ExecuteNonQuery();
                            ids.Add(SqlUtils.GetLastId());
                        }
                    }
                    transaction.Commit();
                }
            }
            return ids;
        }

        public int AssignAllInOne(List<DataRow> rows)
        {
            string query = $"UPDATE Grade SET student_id = @student_id, subject_id = @subject_id, teacher_id = @teacher_id WHERE id = @id";
            int rowsAffected = 0;
            using(var connection = new SqliteConnection(Config.SQLConnectionString))
            {
                connection.Open();
                using(var transaction = connection.BeginTransaction())
                {
                    using(var command = new SqliteCommand(query, connection, transaction))
                    {
                        command.Parameters.Add("@student_id", SqliteType.Integer);
                        command.Parameters.Add("@subject_id", SqliteType.Integer);
                        command.Parameters.Add("@teacher_id", SqliteType.Integer);
                        command.Parameters.Add("@id", SqliteType.Integer);
                        foreach(var row in rows)
                        {
                            command.Parameters["@student_id"].Value = row["student_id"];
                            command.Parameters["@subject_id"].Value = row["subject_id"];
                            command.Parameters["@teacher_id"].Value = row["teacher_id"];
                            command.Parameters["@id"].Value = row["grade_id"];
                            rowsAffected += command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
            }
            return rowsAffected;
        }

        public List<DataRow> GetByStudentId(int student_id)
        {
            string query = $"SELECT * FROM {TableName} WHERE student_id = @student_id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@student_id", student_id)
            };
            return SqlUtils.MakeQuery(query, parameters).Rows.Cast<DataRow>().ToList();
        }

        public List<DataRow> GetByStudentIdAndSubjectId(int student_id, int subject_id)
        {
            string query = $"SELECT * FROM {TableName} WHERE student_id = @student_id AND subject_id = @subject_id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@student_id", student_id),
                new SqliteParameter("@subject_id", subject_id)
            };
            return SqlUtils.MakeQuery(query, parameters).Rows.Cast<DataRow>().ToList();
        }

        public int GetStudentID(int grade_id)
        {
            string query = $"SELECT student_id FROM {TableName} WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@id", grade_id)
            };
            return Convert.ToInt32(SqlUtils.MakeQuery(query, parameters).Rows[0]["student_id"]);
        }

        public int GetSubjectID(int grade_id)
        {
            string query = $"SELECT subject_id FROM {TableName} WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@id", grade_id)
            };
            return Convert.ToInt32(SqlUtils.MakeQuery(query, parameters).Rows[0]["subject_id"]);
        }

        public int GetTeacherID(int grade_id)
        {
            string query = $"SELECT teacher_id FROM {TableName} WHERE id = @id";
            List<SqliteParameter> parameters = new List<SqliteParameter>()
            {
                new SqliteParameter("@id", grade_id)
            };
            return Convert.ToInt32(SqlUtils.MakeQuery(query, parameters).Rows[0]["teacher_id"]) ;
        }

        

    }
}