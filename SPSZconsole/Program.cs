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
            // JUST FOR DEVELOPMENT PURPOSES


            // SPSZDomainLayer.FillDatabaseWithDemoData.Execute();
            var row = Config.Connection.SubjectTG.GetById(9);
            var subject = SubjectMapper.FromRow(row);
            Console.WriteLine(subject);
        }
    }
}
