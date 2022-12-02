using System;
using SPSZDataLayer.TableGateway.Interface;
using SPSZDataLayer.TableGateway.Sql;

namespace SPSZDataLayer
{
    public class SqlConnection : IDataConnection
    {
        public ISubjectTG SubjectTG { get; } = new SubjectSqlTG();
    }
}