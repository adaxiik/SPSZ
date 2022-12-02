using System;

namespace SPSZDomainLayer.Model
{
    public class Grade : IEntity
    {
        public int? Id { get; set; }
        public int Value { get; set; }
        public int Weight { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}