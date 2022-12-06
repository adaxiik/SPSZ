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
    public class GradeProposeService
    {
        public static int GetProposedGrade(int studentId, int subjectId, double rounding)
        {
            var gradeFilter = new GradeFilter();
            gradeFilter.Load(studentId);
            var grades = gradeFilter.GetGradesBySubjectId(subjectId);
            int sum = 0;
            int weightSum = 0;
            foreach (var grade in grades)
            {
                sum += grade.Value * grade.Weight;
                weightSum += grade.Weight;
            }
            if (sum == 0)
            {
                return 0;
            }
            else
            {
                var average = (double)sum / weightSum;
            
                var temp = average - Math.Floor(average);

                var proposedGrade = Math.Floor(average) + (temp > rounding ? 0:1);

                return (int)proposedGrade;
            }
        }
    }
}