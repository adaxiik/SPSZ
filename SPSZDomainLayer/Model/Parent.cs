using System;
using System.Collections.Generic;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;

namespace SPSZDomainLayer.Model
{
    public class Parent : Person, IMailable
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Student> Students { get; set; }
        public Parent()
        {
        }

        public List<Student> GetStudents()
        {

            if (Id is null || Id == -1)
                return Students;
        

            var data = Config.Connection.StudentTG.GetStudentsByParentId(Id.Value);
            Students = StudentMapper.FromRows(data);
            return Students;
        }

        public static Parent GetByID(int id)
        {
            var data = Config.Connection.ParentTG.GetById(id);
            return ParentMapper.FromRow(data);
        }

        public static List<Parent> GetAll()
        {
            var data = Config.Connection.ParentTG.GetAll();
            return ParentMapper.FromRows(data);
        }
    }
}