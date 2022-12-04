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

        public static List<Mail> GetMailsOfUser(int userId)
        {
            var mails = MailMapper.FromRows(Config.Connection.MailboxTG.GetAllMailsOfRecepient(userId));
            mails.Sort((x, y) => y.Date.CompareTo(x.Date));
            return mails;
        }

        public static Mail GetMailById(int id)
        {
            var mail = MailMapper.FromRow(Config.Connection.MailboxTG.GetById(id));

            return mail;
        }


        public static bool SendMail(Mail mail, int recepientId, int senderId, out string errorMessage)
        {
            errorMessage = string.Empty;
            if(string.IsNullOrWhiteSpace(mail.Subject))
            {
                errorMessage = "Předmět zprávy je povinný údaj";
                return false;
            }

            if(string.IsNullOrWhiteSpace(mail.Message))
            {
                errorMessage = "Zpráva je povinný údaj";
                return false;
            }

            if(mail.Subject.Length > 50)
            {
                errorMessage = "Předmět zprávy nesmí být delší než 50 znaků";
                return false;
            }

            if(mail.Message.Length > 500)
            {
                errorMessage = "Zpráva nesmí být delší než 500 znaků";
                return false;
            }

            var r = MailMapper.ToRow(mail);

            var recepient = LoginSystemService.Instance.LogedAs is Parent ? Config.Connection.TeacherTG.GetById(recepientId) : Config.Connection.ParentTG.GetById(recepientId);
            if(recepient is null)
            {
                errorMessage = "Příjemce zprávy neexistuje";
                return false;
            }

            var sender = LoginSystemService.Instance.LogedAs is Parent ? Config.Connection.ParentTG.GetById(senderId) : Config.Connection.TeacherTG.GetById(senderId);
            if(sender is null)
            {
                errorMessage = "Pravděpodobně jste byl odhlášen ze systému";
                return false;
            }

            int mid = Config.Connection.MailboxTG.Insert(r);

            Config.Connection.MailboxTG.AssignSenderAndRecepient(mid, senderId, recepientId);
            return true;
        }
    }
}