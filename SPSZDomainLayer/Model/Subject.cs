using System;
using System.Collections.Generic;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;

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

        public static List<Subject> GetAllSubjects()
        {
            return SubjectMapper.FromRows(Config.Connection.SubjectTG.GetAll());
        }
        public static Subject GetByID(int id)
        {
            var data = Config.Connection.SubjectTG.GetById(id);
            return SubjectMapper.FromRow(data);
        }

        public override string ToString()
        {
            return $"Subject: {Id} {Name} {Description} {Label}";
        }
        
    }
}