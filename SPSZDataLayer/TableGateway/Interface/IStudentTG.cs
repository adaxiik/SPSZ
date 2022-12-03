using System;
using System.Collections.Generic;
using System.Data;

namespace SPSZDataLayer.TableGateway.Interface
{
    public interface IStudentTG : ITableGateway
    {
        public int SetParentId(int id, int parent_id);
        public int SetClassId(int id, int class_id);
        public List<DataRow> GetStudentsByParentId(int parent_id);
        public List<DataRow> GetStudentsByClassId(int class_id);
        public int GetStudentClassId(int id);
    }
}