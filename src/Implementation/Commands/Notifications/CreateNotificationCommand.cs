using System;
using System.Collections.Generic;
using System.Text;
using Core.Database.Enums;
using Core.Database.Models;

namespace Core.Users.Implementation.Commands.Notifications
{
    public class CreateNotificationCommand
    {
        public Guid UserId { get; set; }
        public NotificationPayloadModel Payload { get; set; }
    }
}
