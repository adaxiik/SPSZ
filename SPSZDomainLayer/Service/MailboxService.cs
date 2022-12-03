using System.Runtime.CompilerServices;
using System;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;
using SPSZDomainLayer.Model;
using SPSZDataLayer.TableGateway;
using System.Collections.Generic;
using System.Linq;
using SPSZDomainLayer.TransactionScript;

namespace SPSZDomainLayer.Service
{
    public class MailboxService
    {   
        public static List<IMailable> GetAvailableRecepients()
        {
            var mailables = new List<IMailable>();

            if(LoginSystemService.Instance.LogedAs is Parent)
            {
                var teachers = Teacher.GetAll();
                mailables.AddRange(teachers);
            }
            else if(LoginSystemService.Instance.LogedAs is Teacher)
            {
                var parents = Parent.GetAll();
                mailables.AddRange(parents);
            }
            return mailables;
        }
        public static bool SendMail(Mail mail, int recepientId, int senderId)
        {
            var r = MailMapper.ToRow(mail);
            int mid = Config.Connection.MailboxTG.Insert(r);
            Config.Connection.MailboxTG.AssignSenderAndRecepient(mid, senderId, recepientId);
            return true;
        }
    }
}