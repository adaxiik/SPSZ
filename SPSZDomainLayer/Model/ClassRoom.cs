using System;
using System.Collections.Generic;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;

namespace SPSZDomainLayer.Model
{
    public class ClassRoom : IEntity
    {
        public int? Id { get; set; }
        public string ClassName { get; set; }
        public List<Student> Students { get; set; }

        public List<Student> GetStudents()
        {
            Students = new List<Student>();
            if (Id is null || Id == -1)
                return Students;
            var data = Config.Connection.StudentTG.GetStudentsByClassId(Id.Value);
            Students = StudentMapper.FromRows(data);
            return Students;
        }

        public static ClassRoom GetByID(int id)
        {
            return ClassMapper.FromRow(Config.Connection.ClassRoomTG.GetById(id));
        }
    }
}