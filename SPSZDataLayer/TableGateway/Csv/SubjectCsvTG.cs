using System;
using System.Collections.Generic;
using System.Data;
using SPSZDataLayer.TableGateway.Interface;

namespace SPSZDataLayer.TableGateway.Csv
{
    public class SubjectCsvTG : ISubjectTG
    {
        public string TableName => throw new NotImplementedException();

        public List<DataRow> GetAll()
        {
            throw new NotImplementedException();
        }

        public DataRow GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(DataRow row)
        {
            throw new NotImplementedException();
        }

        public int Update(DataRow row)
        {
            throw new NotImplementedException();
        }

        public int DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }
        
    }
}