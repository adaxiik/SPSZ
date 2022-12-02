using System;

namespace SPSZDomainLayer.Model
{
    public class Subject : IEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }

        public Subject()
        {
        }

        public Subject(int id, string name, string description, string label)
        {
            Id = id;
            Name = name;
            Description = description;
            Label = label;
        }
    }
}