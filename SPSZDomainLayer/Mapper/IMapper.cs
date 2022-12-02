using System.Collections.Generic;
using System;
using System.Data;

namespace SPSZDomainLayer.Mapper
{
    public interface IMapper<T> where T : IEntity
    {
        public static T FromRow(DataRow row) => throw new Exception("Unimplemented");
        public static List<T> FromRows(List<DataRow> rows) => throw new Exception("Unimplemented");
        public static DataRow ToRow(T entity) => throw new Exception("Unimplemented");
        public static List<DataRow> ToRows(List<T> entities) => throw new Exception("Unimplemented");
    }
    
}