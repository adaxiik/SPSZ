using System;

namespace SPSZDataLayer.TableGateway.Interface
{
    public interface IStudentTG : ITableGateway
    {
        public int SetParentId(int id, int parent_id);
        public int SetClassId(int id, int class_id);
    }
}