using System;
using System.Collections.Generic;

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
            Students = new List<Student>(); 
            // TODO: implement
            return Students;
        }


    }
}