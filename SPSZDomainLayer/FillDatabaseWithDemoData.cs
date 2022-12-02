using System.Data.Common;
using System.Security.Cryptography;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;
using SPSZDomainLayer.Model;
using SPSZDataLayer.TableGateway;
using System.Data;

namespace SPSZDomainLayer
{
    public class FillDatabaseWithDemoData
    {
        static void ClearAllTables()
        {
            Config.Connection.SubjectTG.DeleteAll();
            Config.Connection.ClassRoomTG.DeleteAll();
            Config.Connection.TeacherTG.DeleteAll();
            Config.Connection.StudentTG.DeleteAll();
            Config.Connection.GradeTG.DeleteAll();
            Config.Connection.ParentTG.DeleteAll();

            DataUtils.ResetIndexing("Person");
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

        static void FillTeachers()
        {
            var teachers = new List<Teacher>
            {
                new Teacher(){
                    Firstname = "John",
                    Lastname = "Doe",
                    Email = "john.doe@mail.com",
                    Password = DataUtils.ToMD5("12345678")
                },
                new Teacher(){
                    Firstname = "Jane",
                    Lastname = "Doe",
                    Email = "jane.doe@mail.com",
                    Password = DataUtils.ToMD5("Ree")
                },
                new Teacher(){
                    Firstname = "Jožko",
                    Lastname = "Mrkvička",
                    Email = "jozanek.mrkvicka@mail.com",
                    Password = DataUtils.ToMD5("1234")
                }
            };
            foreach (var teacher in teachers)
                Config.Connection.TeacherTG.Insert(TeacherMapper.ToRow(teacher));

        }

        static void FillParents()
        {
            var parents = new List<Parent>
            {
                new Parent(){
                    Firstname = "Jack",
                    Lastname = "Kerouac",
                    Email = "jeck.k@mail.com",
                    Password = DataUtils.ToMD5("1234")
                },
                new Parent(){
                    Firstname = "Arthur",
                    Lastname = "Rimbaud",
                    Email = "ar@mail.com",
                    Password = DataUtils.ToMD5("6969")
                },
                new Parent(){
                    Firstname = "William",
                    Lastname = "Blake",
                    Email = "willablejk@mail.com",
                    Password = DataUtils.ToMD5("ahoj")
                }
            };
            foreach (var parent in parents)
                Config.Connection.ParentTG.Insert(ParentMapper.ToRow(parent));

        }

        static void FillStudents()
        {
            var students = new List<Student>
            {
                // add 15 students of parents 
                new Student(){
                    Firstname = "John",
                    Lastname = "Rimbaud",
                    Address = "Doe street 1"
                },
                new Student(){
                    Firstname = "Yoko",
                    Lastname = "Rimbaud",
                    Address = "Doe street 1"
                },
                new Student(){
                    Firstname = "Adam",
                    Lastname = "Blake",
                    Address = "Avenue 1"
                },
                new Student(){
                    Firstname = "Jane",
                    Lastname = "Blake",
                    Address = "Avenue 1"
                },
                new Student(){
                    Firstname = "Zoe",
                    Lastname = "Blake",
                    Address = "Avenue 1"
                },
                new Student(){
                    Firstname = "Tom",
                    Lastname = "Kerouac",
                    Address = "Klimkovice 1"
                },
                new Student(){
                    Firstname = "Liza",
                    Lastname = "Kerouac",
                    Address = "Klimkovice 1"
                },
                new Student(){
                    Firstname = "Linda",
                    Lastname = "Kerouac",
                    Address = "Klimkovice 1"
                },
            };
            foreach (var student in students)
                Config.Connection.StudentTG.Insert(StudentMapper.ToRow(student));
        }

        static void LinkParents()
        {
            List<Student> students = StudentMapper.FromRows(Config.Connection.StudentTG.GetAll());

            foreach (var student in students)
            {
                var matchingParents = ParentMapper.FromRows(Config.Connection.ParentTG.GetByLastname(student.Lastname));
                if (matchingParents.Count <= 0)
                    throw new Exception("No matching parent found for student " + student.Firstname + " " + student.Lastname);

                var parent = matchingParents[0];
                Config.Connection.StudentTG.SetParentId((int)student.Id, (int)parent.Id);
            }

        }
        static void FillClasses()
        {
            var classes = new List<ClassRoom>
            {
                new ClassRoom()
                {
                    ClassName = "1.A",
                },
                new ClassRoom()
                {
                    ClassName = "2.B",
                },
            };
            foreach (var classRoom in classes)
                Config.Connection.ClassRoomTG.Insert(ClassMapper.ToRow(classRoom));
        }

        static void LinkStudents()
        {
            List<Student> students = StudentMapper.FromRows(Config.Connection.StudentTG.GetAll());
            int numberOfClasses = Config.Connection.ClassRoomTG.GetAll().Count;

            // fill classes zig-zag
            for (int i = 0; i < students.Count; i++)
            {
                int classId = (i % numberOfClasses) + 1;
                Config.Connection.StudentTG.SetClassId((int)students[i].Id, classId);
            }
        }

        static void FillGrades(int minPerSubject, int maxPerSubject)
        {

            List<DataRow> gradeInfos = new List<DataRow>();
            var gradeInfoScheme = new DataTable();
            gradeInfoScheme.Columns.Add("student_id", typeof(int));
            gradeInfoScheme.Columns.Add("subject_id", typeof(int));
            gradeInfoScheme.Columns.Add("teacher_id", typeof(int));
            gradeInfoScheme.Columns.Add("grade_id", typeof(int));


            List<DataRow> toAdd = new List<DataRow>();

            List<Student> students = StudentMapper.FromRows(Config.Connection.StudentTG.GetAll());
            List<Subject> subjects = SubjectMapper.FromRows(Config.Connection.SubjectTG.GetAll());
            int teacherCount = Config.Connection.TeacherTG.GetAll().Count;

            Random random = new Random(42069);

            foreach (var student in students)
            {
                foreach (var subject in subjects)
                {
                    int numberOfGrades = random.Next(minPerSubject, maxPerSubject);
                    for (int i = 0; i < numberOfGrades; i++)
                    {
                        var grade = new Grade()
                        {
                            Value = random.Next(1, 6),
                            Date = DateTime.Now.AddDays(-random.Next(0, 365)),
                            Weight = random.Next(1, 11),

                        };
                        toAdd.Add(GradeMapper.ToRow(grade));

                        var r = gradeInfoScheme.NewRow();
                        r["student_id"] = student.Id;
                        r["subject_id"] = subject.Id;
                        r["teacher_id"] = random.Next(1, teacherCount + 1);
                        gradeInfos.Add(r);

                    }
                }
            }

            Config.Connection.GradeTG.Insert(toAdd);
            for(int i = 0; i < gradeInfos.Count; i++)
            {
                gradeInfos[i]["grade_id"] = i + 1;
            }
            Config.Connection.GradeTG.AssignAllInOne(gradeInfos);

        }

        static void LOG(string message)
        {
            Console.WriteLine($"LOG[{DateTime.Now.ToString("HH:mm:ss")}] {message}");
        }
        public static void Execute()
        {
            LOG("Clearing database");
            ClearAllTables();
            LOG("Filling subjects");
            FillSubjects();
            LOG("Filling teachers");
            FillTeachers();
            LOG("Filling parents");
            FillParents();
            LOG("Filling students");
            FillStudents();
            LOG("Linking parents");
            LinkParents();
            LOG("Filling classes");
            FillClasses();
            LOG("Linking students");
            LinkStudents();
            LOG("Filling grades");
            FillGrades(5, 10);
            LOG("Done");
        }
    }
}