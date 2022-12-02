using System;
using SPSZDataLayer.TableGateway;
using SPSZDataLayer.TableGateway.Csv;
using SPSZDataLayer.TableGateway.Interface;

namespace SPSZDataLayer
{
    public class CsvConnection : IDataConnection
    {
        public ISubjectTG SubjectTG  { get; } = new SubjectCsvTG();

    }
}