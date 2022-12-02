using System;
using System.Collections.Generic;
using System.Data;
namespace SPSZDataLayer.TableGateway
{
    public interface ITableGateway
    {
        public List<DataRow> GetAll();
        public DataRow GetById(int id);
        public int Insert(DataRow row);
        public int Update(DataRow row);
        public int DeleteById(int id);
        public void DeleteAll();
    }
}