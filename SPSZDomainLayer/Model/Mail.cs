using System;
using System.Collections.Generic;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;
using SPSZDomainLayer.Service;

namespace SPSZDomainLayer.Model
{
    public class Mail : IEntity
    {
        public int? Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public IMailable GetRecepient()
        {
            throw new NotImplementedException();
        }
        
        public IMailable GetSender()
        {
            if (LoginSystemService.Instance.LogedAs is Parent)
                return TeacherMapper.FromRow(Config.Connection.TeacherTG.GetById(Config.Connection.MailboxTG.GetSenderId(Id.Value)));
            
            if (LoginSystemService.Instance.LogedAs is Teacher)
                return ParentMapper.FromRow(Config.Connection.ParentTG.GetById(Config.Connection.MailboxTG.GetSenderId(Id.Value)));
            
            return null;
        }
        
    }
}