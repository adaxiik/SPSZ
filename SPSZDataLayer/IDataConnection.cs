using System;
using SPSZDataLayer.TableGateway;
using SPSZDataLayer.TableGateway.Interface;

namespace SPSZDataLayer
{
    public interface IDataConnection
    {
        ISubjectTG SubjectTG { get; }
        ITeacherTG TeacherTG { get; }
        IClassRoomTG ClassRoomTG { get; }
        IStudentTG StudentTG { get; }
        IParentTG ParentTG { get; }
        IGradeTG GradeTG { get; }
        IMailboxTG MailboxTG { get; }
        
    }
}