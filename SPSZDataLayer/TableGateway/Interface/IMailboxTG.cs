using System;
using System.Collections.Generic;
using System.Data;

namespace SPSZDataLayer.TableGateway.Interface
{
    public interface IMailboxTG: ITableGateway
    {
        void AssignSender(int id, int senderId);
        void AssignRecepient(int id, int recepientId);
        void AssignSenderAndRecepient(int id, int senderId, int recepientId);
    }
}