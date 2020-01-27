using System;
using System.Collections.Generic;
using System.Text;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Tables;
using Core.Users.Implementation.Commands.Notifications;
using Core.Users.Interfaces.Services;
using Newtonsoft.Json;

namespace Core.Users.Implementation.Services
{
    public class NotificationService : INotificationService
    {
        private IBeawreContext _beawreContext;

        public NotificationService(IBeawreContext beawreContext)
        {
            _beawreContext = beawreContext;
        }

        public bool Create(CreateNotificationCommand command)
        {
            _beawreContext.Relationship.Add(new Relationship()
            {
                FromType = ObjectType.User, 
                ToType = ObjectType.Notification,
                FromId = command.UserId, 
                ToId = Guid.Empty,
                Payload = JsonConvert.SerializeObject(command.Payload)
            });
            _beawreContext.SaveChanges();
            return true;
        }
       
    }
}
