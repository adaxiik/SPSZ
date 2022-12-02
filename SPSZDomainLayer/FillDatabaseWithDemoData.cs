using System.Security.Cryptography;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;
using SPSZDomainLayer.Model;
using SPSZDataLayer.TableGateway;

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

        static string PasswordToMD5(string password)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        static void FillTeachers()
        {
            var teachers = new List<Teacher>
            {
                new Teacher(){
                    Firstname = "John",
                    Lastname = "Doe", 
                    Email = "john.doe@mail.com",
                    Password = PasswordToMD5("12345678")
                },
                new Teacher(){
                    Firstname = "Jane",
                    Lastname = "Doe", 
                    Email = "jane.doe@mail.com",
                    Password = PasswordToMD5("Ree")
                },
                new Teacher(){
                    Firstname = "Jožko",
                    Lastname = "Mrkvička", 
                    Email = "jozanek.mrkvicka@mail.com",
                    Password = PasswordToMD5("1234")
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
                    Password = PasswordToMD5("1234")
                },
                new Parent(){
                    Firstname = "Arthur",
                    Lastname = "Rimbaud",
                    Email = "ar@mail.com",
                    Password = PasswordToMD5("6969")
                },
                new Parent(){
                    Firstname = "William",
                    Lastname = "Blake",
                    Email = "willablejk@mail.com",
                    Password = PasswordToMD5("ahoj")
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
                Console.WriteLine(student.Firstname + " " + student.Lastname + " ID: " + student.Id);
                
            }

            foreach (var student in students)
            {
                var matchingParents = ParentMapper.FromRows(Config.Connection.ParentTG.GetByLastname(student.Lastname));
                if (matchingParents.Count <= 0)
                    throw new Exception("No matching parent found for student " + student.Firstname + " " + student.Lastname);
                
                var parent = matchingParents[0];
                Config.Connection.StudentTG.SetParentId((int)student.Id,(int) parent.Id);
            }

        }
        public static void Execute()
        {
            ClearAllTables();
            FillSubjects();
            FillTeachers();
            FillParents();
            FillStudents();
            LinkParents();
        }
    }
}