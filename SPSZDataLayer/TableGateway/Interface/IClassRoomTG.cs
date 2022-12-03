using System.Collections.Generic;
using System;
using System.Data;

namespace SPSZDataLayer.TableGateway.Interface
{
    public interface IClassRoomTG : ITableGateway
    {
        List<DataRow> GetClassRoomsByTeacherId(int teacherId);
        int AssignClassRoomToTeacher(int teacherId, int classRoomId);
    }
}