using System.Runtime.CompilerServices;
using System;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;
using SPSZDomainLayer.Model;
using SPSZDataLayer.TableGateway;
using System.Collections.Generic;
using System.Linq;

namespace SPSZDomainLayer.Service
{
    public class GradeFilter
    {
        struct GradeInfo
        {
            public List<Grade> Grades;
            public int SubjectId;
        }
        List<GradeInfo> GradesInfo = new List<GradeInfo>();
        public List<Subject> Subjects { get; private set; }
        public void Load(int studentId)
        {
            Subjects = SubjectMapper.FromRows(Config.Connection.SubjectTG.GetAll());
            var grades = GradeMapper.FromRows(Config.Connection.GradeTG.GetByStudentId(studentId));
            foreach (var subject in Subjects)
            {
                var gradesInfo = new GradeInfo();
                gradesInfo.SubjectId = subject.Id.Value;
                gradesInfo.Grades = GradeMapper.FromRows(Config.Connection.GradeTG.GetByStudentIdAndSubjectId(studentId, subject.Id.Value));
                GradesInfo.Add(gradesInfo);
            }

        }

        public List<Grade> GetGradesBySubjectId(int subjectId)
        {
            return GradesInfo.Where(x => x.SubjectId == subjectId).Select(x => x.Grades).FirstOrDefault();
        }

        public List<Grade> GetBySubjectAndWeight(int subjectId, int weight)
        {
            return GradesInfo.Where(x => x.SubjectId == subjectId).Select(x => x.Grades).FirstOrDefault().Where(x => x.Weight == weight).ToList();
        }
    }
}