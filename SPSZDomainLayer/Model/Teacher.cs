using System.Collections.Generic;
using System;

namespace SPSZDomainLayer.Model
{
    public class Teacher : Person
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
            // TODO: implement
            return ClassRooms;
        }
    }
}