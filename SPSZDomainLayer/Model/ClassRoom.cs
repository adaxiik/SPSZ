using System;
using System.Collections.Generic;

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
            // TODO: implement
            return Students;
        }
    }
}