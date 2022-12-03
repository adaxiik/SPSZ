using System;
using SPSZDataLayer.TableGateway;
using SPSZDataLayer.TableGateway.Csv;
using SPSZDataLayer.TableGateway.Interface;

namespace SPSZDataLayer
{
    public class CsvConnection : IDataConnection
    {
        public ISubjectTG SubjectTG { get; } = new SubjectCsvTG();
        public IClassRoomTG ClassRoomTG { get; } = new ClassRoomCsvTG();
        public ITeacherTG TeacherTG { get; } = new TeacherCsvTG();
        public IStudentTG StudentTG { get; } = new StudentCsvTG();

        public IParentTG ParentTG { get; } = new ParentCsvTG();

        public IGradeTG GradeTG { get; } = new GradeCsvTG();

        public IMailboxTG MailboxTG { get; } = new MailboxCsvTG();
    }
}