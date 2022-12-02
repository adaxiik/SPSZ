using System;
using System.Collections.Generic;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;

namespace SPSZDomainLayer.Model
{
    public class Parent : Person
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


    }
}