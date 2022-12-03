using System.Collections.Generic;
using System;
using SPSZDomainLayer.Mapper;
using SPSZDataLayer.GlobalConfig;
namespace SPSZDomainLayer.Model
{
    public class Student : Person
    {
        public string Address { get; set; }

        public Student()
        {
        }

        public ClassRoom GetStudentClass()
        {
            if (Id is null || Id == -1)
            {
                throw new Exception("Student has no id");
            }
            var i = Config.Connection.StudentTG.GetStudentClassId(Id.Value);
            return ClassMapper.FromRow(Config.Connection.ClassRoomTG.GetById(i));
        }

        public static Student GetByID(int id)
        {
            return StudentMapper.FromRow(Config.Connection.StudentTG.GetById(id));
        }

    }
}