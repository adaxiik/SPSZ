using System;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;

namespace SPSZDomainLayer.Model
{
    public class Grade : IEntity
    {
        public int? Id { get; set; }
        public int Value { get; set; }
        public int Weight { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public static Grade GetByID(int id)
        {
            var data = Config.Connection.GradeTG.GetById(id);
            return GradeMapper.FromRow(data);
        }

        public static int GetStudentID(int grade_id)
        {
            return Config.Connection.GradeTG.GetStudentID(grade_id);
        }

        public static int GetSubjectID(int grade_id)
        {
            return Config.Connection.GradeTG.GetSubjectID(grade_id);
        }

        public static int GetTeacherID(int grade_id)
        {
            return Config.Connection.GradeTG.GetTeacherID(grade_id);
        }

        
    }
}