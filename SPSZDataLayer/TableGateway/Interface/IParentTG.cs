using System;
using System.Collections.Generic;
using System.Data;

namespace SPSZDataLayer.TableGateway.Interface
{
    public interface IParentTG : ITableGateway
    {
        public List<DataRow> GetByLastname(string lastName);
    }
}