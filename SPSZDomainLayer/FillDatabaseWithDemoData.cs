using System;
using System.Collections.Generic;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;
using SPSZDomainLayer.Model;

namespace SPSZDomainLayer
{
    public class FillDatabaseWithDemoData
    {
        static void ClearAllTables()
        {
            Config.Connection.SubjectTG.DeleteAll();
            // add more
        }

        static void FillSubjects()
        {
            var subjects = new List<Subject>
            {
                new Subject(){Name = "Mathematics", Description = "Mathematics is the study of quantity, structure, space, and change.", Label = "Math"},
                new Subject(){Name = "Physics", Description = "Physics is the natural science that involves the study of matter and its motion through space and time, along with related concepts such as energy and force.", Label = "Phys"},
                new Subject(){Name = "Chemistry", Description = "Chemistry is a branch of physical science that studies the composition, structure, properties and change of matter.", Label = "Chem"},
                new Subject(){Name = "Biology", Description = "Biology is the natural science that studies life and living organisms, including their physical structure, chemical processes, molecular interactions, physiological mechanisms, development and evolution.", Label = "Bio"},
                new Subject(){Name = "Geography", Description = "Geography is a field of science devoted to the study of the lands, the features, the inhabitants, and the phenomena of Earth.", Label = "Geo"},
                new Subject(){Name = "History", Description = "History is the study of the past as it is described in written documents.", Label = "Hist"},
                new Subject(){Name = "English", Description = "English is a West Germanic language that was first spoken in early medieval England and eventually became a global lingua franca.", Label = "Eng"},
                new Subject(){Name = "German", Description = "German is a West Germanic language that is mainly spoken in Central Europe.", Label = "Ger"}
            };
            foreach (var subject in subjects)
                Config.Connection.SubjectTG.Insert(SubjectMapper.ToRow(subject));

        }

        public static void Execute()
        {
            ClearAllTables();
            FillSubjects();
        }
    }
}