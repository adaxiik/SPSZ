using System.Runtime.CompilerServices;
using System;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;
using SPSZDomainLayer.Model;
using SPSZDataLayer.TableGateway;
using System.Collections.Generic;
using System.Linq;
using SPSZDomainLayer.TransactionScript;

namespace SPSZDomainLayer.Service
{
    public class GradeExportService
    {
        public static string ExportToCsv(int studentId)
        {
            List<string> rows = new List<string>();
            
            string header = "Předmět; Známka; Váha; Datum; Poznámka";
            rows.Add(header);

            var gradeFilter = new GradeFilter();
            gradeFilter.Load(studentId);
            var subjects = gradeFilter.Subjects;
            
            foreach (var subject in subjects)
            {
                var grades = gradeFilter.GetGradesBySubjectId(subject.Id.Value);
                foreach (var grade in grades)
                {
                    string row = $"{subject.Name}; {grade.Value}; {grade.Weight}; {grade.Date}; {grade.Description}";
                    rows.Add(row);
                }
            }

            return string.Join(Environment.NewLine, rows);
        }

        public static string ExportToTxt(int studentId)
        {
            List<string> rows = new List<string>();
            
            string header = string.Format("{0,-20} | {1,-10} | {2,-10} | {3,-10} | {4,-10}", "Předmět", "Známka", "Váha", "Datum", "Poznámka");
            rows.Add(header);
            rows.Add(new string('-', header.Length));

            var gradeFilter = new GradeFilter();
            gradeFilter.Load(studentId);
            var subjects = gradeFilter.Subjects;
            
            foreach (var subject in subjects)
            {
                var grades = gradeFilter.GetGradesBySubjectId(subject.Id.Value);
                foreach (var grade in grades)
                {
                    string row = $"{subject.Name,-20} | {grade.Value,-10} | {grade.Weight,-10} | {grade.Date.ToShortDateString(),-10} | {grade.Description,-10}";
                    rows.Add(row);
                }
            }

            return string.Join(Environment.NewLine, rows);
        }
    }
}