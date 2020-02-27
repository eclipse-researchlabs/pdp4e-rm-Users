using System;
using System.Collections.Generic;
using System.Text;
using Core.Database.Enums;
using Core.Database.Models;
using MediatR;

namespace Core.Users.Implementation.Commands.Notifications
{
    public class CreateNotificationCommand : IRequest<bool>
    {
        public Guid[] UserId { get; set; }
        public NotificationPayloadModel Payload { get; set; }
    }
}
