using System;
using System.Collections.Generic;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;

namespace SPSZDomainLayer.Model
{
    public class Mail : IEntity
    {
        public int? Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public IMailable GetSender()
        {
            throw new NotImplementedException();
        }

        public IMailable GetRecepient()
        {
            throw new NotImplementedException();
        }
    }
}