using System.Collections.Generic;
using System;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;

namespace SPSZDomainLayer.Model
{
    public class Teacher : Person, IMailable
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public List<ClassRoom> ClassRooms { get; set; }
        public Teacher()
        {
        }

        public List<ClassRoom> GetClassRooms()
        {
            ClassRooms = new List<ClassRoom>();
            if (Id is null || Id == -1)
                return ClassRooms;
        
            var data = Config.Connection.ClassRoomTG.GetClassRoomsByTeacherId(Id.Value);
            ClassRooms = ClassMapper.FromRows(data);

            return ClassRooms;
        }

        public static Teacher GetByID(int id)
        {
            var data = Config.Connection.TeacherTG.GetById(id);
            return TeacherMapper.FromRow(data);
        }

        public static List<Teacher> GetAll()
        {
            var data = Config.Connection.TeacherTG.GetAll();
            return TeacherMapper.FromRows(data);
        }
    }
}