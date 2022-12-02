using System;

namespace SPSZDataLayer.TableGateway.Interface
{
    public interface IStudentTG : ITableGateway
    {
        public int SetParentId(int id, int parentId);
    }
}