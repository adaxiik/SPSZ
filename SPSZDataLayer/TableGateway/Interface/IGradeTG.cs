using System.Data;
using System;
using System.Collections.Generic;


namespace SPSZDataLayer.TableGateway.Interface
{
    public interface IGradeTG : ITableGateway
    {
        public int AssignGradeToStudent(int student_id, int grade_id);
        public int AssignGradeToSubject(int subject_id, int grade_id);
        public int AssignGradeToTeacher(int teacher_id, int grade_id);
        public List<int> Insert(List<DataRow> rows);
        public int AssignAllInOne(List<DataRow> rows);

        public int AssignAllInOne(int student_id, int subject_id, int teacher_id, int grade_id);
        public List<DataRow> GetByStudentId(int student_id);
        public List<DataRow> GetByStudentIdAndSubjectId(int student_id, int subject_id);
        public int GetStudentID(int grade_id);
        public int GetSubjectID(int grade_id);
        public int GetTeacherID(int grade_id);
    }
}