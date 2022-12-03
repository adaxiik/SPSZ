using System;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;
using SPSZDomainLayer.Model;

namespace SPSZconsole
{
    class Program
    {
        static void Main(string[] args)
        {   
            // SPSZDomainLayer.FillDatabaseWithDemoData.Execute();
            var row = Config.Connection.SubjectTG.GetById(9);
            var subject = SubjectMapper.FromRow(row);
            Console.WriteLine(subject);

            // var s = new Subject(){Name = "Hudebka", Description = "Zpíváme si", Label = "Hudba"};
            // Config.Connection.SubjectTG.Insert(SubjectMapper.ToRow(s));
        }
    }
}
