using System;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;

namespace SPSZDomainLayer.Model
{
    public interface IMailable : IEntity
    {
        string Email { get; set; }   
    }
}