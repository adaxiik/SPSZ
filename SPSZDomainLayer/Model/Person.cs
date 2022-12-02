using System;

namespace SPSZDomainLayer.Model
{
    public class Person : IEntity
    {
        public int? Id { get; set; }
        public string Firstname { get; set;}
        public string Lastname { get; set;}

        public Person(int? id, string firstname, string lastname)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
        }
        public Person()
        {
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1} {2}", Id, Firstname, Lastname);
        }
    }
}
