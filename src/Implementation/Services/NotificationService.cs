using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Tables;
using Core.Users.Implementation.Commands.Notifications;
using Core.Users.Interfaces.Services;
using MediatR;
using Newtonsoft.Json;

namespace Core.Users.Implementation.Services
{
    public class NotificationService : INotificationService
    {
        private IBeawreContext _beawreContext;
        private IMediator _mediator;

        public NotificationService(IBeawreContext beawreContext, IMediator mediator)
        {
            _beawreContext = beawreContext;
            _mediator = mediator;
        }

        public bool CreateForUsers(CreateNotificationCommand command) => _mediator.Send(command).Result;

        public bool Create(CreateNotificationCommand command)
        {
            _beawreContext.Relationship.Add(new Relationship()
            {
                FromType = ObjectType.User, 
                ToType = ObjectType.Notification,
                FromId = command.UserId.FirstOrDefault(), 
                ToId = Guid.Empty,
                Payload = JsonConvert.SerializeObject(command.Payload)
            });
            _beawreContext.SaveChanges();
            return true;
        }

        public bool MarkAsRead(MarkAsReadCommand command) => _mediator.Send(command).Result;

        public bool Clear(Guid userId)
        {
            var list = _beawreContext.Relationship.Where(x =>
                x.FromType == ObjectType.User && x.ToType == ObjectType.Notification && x.FromId == userId);
            foreach (var item in list)
                item.IsDeleted = true;

            _beawreContext.SaveChanges();
            return true;
        }

    }
}
