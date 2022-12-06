using System.Runtime.CompilerServices;
using System;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;
using SPSZDomainLayer.Model;
using SPSZDataLayer.TableGateway;
using System.Collections.Generic;
using System.Linq;

namespace SPSZDomainLayer.Service
{
    public class GradeAddition
    {
        public static bool AddGrade(Grade grade, int studentId, int subjectId,int teacherID, out string errorMessage)
        {
            if(grade.Value<1 || grade.Value > 5)
            {
                errorMessage = "Známka musí být v rozsahu 1 až 5";
                return false;
            }

            var subjectR = Config.Connection.SubjectTG.GetById(subjectId);
            if (subjectR is null)
            {
                errorMessage = "Předmět nenalezen";
                return false;
            }
            var subject = SubjectMapper.FromRow(subjectR);

            var studentR = Config.Connection.StudentTG.GetById(studentId);
            if (studentR is null)
            {
                errorMessage = "Student nenalezen";
                return false;
            }
            var student = StudentMapper.FromRow(studentR);

            var teacherR = Config.Connection.TeacherTG.GetById(teacherID);
            if (teacherR is null)
            {
                errorMessage = "Učitel nenalezen";
                return false;
            }
            var teacher = TeacherMapper.FromRow(teacherR);

            if (grade.Weight > 10 || grade.Weight < 1)
            {
                errorMessage = "Váha musí být v rozmezí 1 až 10";
                return false;
            }

            int gradeid = Config.Connection.GradeTG.Insert(GradeMapper.ToRow(grade));
            Config.Connection.GradeTG.AssignAllInOne(studentId,subjectId,teacherID,gradeid);
            errorMessage = string.Empty;
            return true;
        }

        public static bool AddGradeToClass(Grade grade, int classId, int subjectId, int teacherID, out string errorMessage)
        {
            if (grade.Value < 1 || grade.Value > 5)
            {
                errorMessage = "Známka musí být v rozsahu 1 až 5";
                return false;
            }

            var subjectR = Config.Connection.SubjectTG.GetById(subjectId);
            if (subjectR is null)
            {
                errorMessage = "Předmět nenalezen";
                return false;
            }
            var subject = SubjectMapper.FromRow(subjectR);

            var classR = Config.Connection.ClassRoomTG.GetById(classId);
            if (classR is null)
            {
                errorMessage = "Třída nenalezena";
                return false;
            }
            var classroom = ClassMapper.FromRow(classR);

            var teacherR = Config.Connection.TeacherTG.GetById(teacherID);
            if (teacherR is null)
            {
                errorMessage = "Učitel nenalezen";
                return false;
            }

            if (grade.Weight > 10 || grade.Weight < 1)
            {
                errorMessage = "Váha musí být v rozmezí 1 až 10";
                return false;
            }

            var students = classroom.GetStudents();

            foreach(var student in students)
            {
                int gradeid = Config.Connection.GradeTG.Insert(GradeMapper.ToRow(grade));
                Config.Connection.GradeTG.AssignAllInOne(student.Id.Value, subjectId, teacherID, gradeid);
            }

            errorMessage = string.Empty;
            return true;
        }

    }
}