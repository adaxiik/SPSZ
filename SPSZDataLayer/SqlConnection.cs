using System;
using SPSZDataLayer.TableGateway.Interface;
using SPSZDataLayer.TableGateway.Sql;

namespace SPSZDataLayer
{
    public class SqlConnection : IDataConnection
    {
        public ISubjectTG SubjectTG { get; } = new SubjectSqlTG();
        public ITeacherTG TeacherTG { get; } = new TeacherSqlTG();
        public IStudentTG StudentTG { get; } = new StudentSqlTG();
        public IParentTG ParentTG { get; } = new ParentSqlTG();
        public IGradeTG GradeTG { get; } = new GradeSqlTG();
        public IClassRoomTG ClassRoomTG { get; } = new ClassRoomSqlTG();
        public IMailboxTG MailboxTG { get; } = new MailboxSqlTG();

    }
}