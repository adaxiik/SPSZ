using System;
using SPSZDataLayer.TableGateway;
using SPSZDataLayer.TableGateway.Interface;

namespace SPSZDataLayer
{
    public interface IDataConnection
    {
        ISubjectTG SubjectTG { get; }
    }
}