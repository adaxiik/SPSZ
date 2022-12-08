using System;
using SPSZDataLayer.TableGateway.Interface;
using SPSZDataLayer.TableGateway.Stub;

namespace SPSZDataLayer
{
    public class StubConnection : IDataConnection
    {
        public ISubjectTG SubjectTG { get; } = new SubjectStubTG();
        public ITeacherTG TeacherTG { get; }
        public IStudentTG StudentTG { get; }
        public IParentTG ParentTG { get; }
        public IGradeTG GradeTG { get; }
        public IClassRoomTG ClassRoomTG { get; }
        public IMailboxTG MailboxTG { get; }

    }
}