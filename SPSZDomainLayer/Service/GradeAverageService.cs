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
    public class GradeAverageService
    {
        public struct GradeAverageInfo
        {
            public Subject Subject;
            public double Average;
        }

        public static List<GradeAverageInfo> GetAverage(int studentId)
        {
            var gradeFilter = new GradeFilter();
            gradeFilter.Load(studentId);
            var subjects = gradeFilter.Subjects;

            var result = new List<GradeAverageInfo>();

            foreach (var subject in subjects)
            {
                var grades = gradeFilter.GetGradesBySubjectId(subject.Id.Value);
                int sum = 0;
                int weightSum = 0;
                foreach (var grade in grades)
                {
                    sum += grade.Value * grade.Weight;
                    weightSum += grade.Weight;
                }
                if(sum == 0)
                    result.Add(new GradeAverageInfo() { Subject = subject, Average = 0 });
                else
                    result.Add(new GradeAverageInfo() { Subject = subject, Average = (double)sum / weightSum });   
            }

            return result;
        }
        public static double GetGradeAverageAll(int studentId)
        {
            var gradeRows = Config.Connection.GradeTG.GetByStudentId(studentId);
            var grades = GradeMapper.FromRows(gradeRows);
            int sum = 0;
            int weightSum = 0;
            foreach (var grade in grades)
            {
                sum += grade.Value * grade.Weight;
                weightSum += grade.Weight;
            }
            if(sum == 0)
            {
                return 0;
            }
            return (double)sum / weightSum;
        }
    }
}