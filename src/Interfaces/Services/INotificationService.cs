using System;
using System.Collections.Generic;
using System.Text;
using Core.Users.Implementation.Commands.Notifications;

namespace Core.Users.Interfaces.Services
{
    public interface INotificationService
    {
        bool Create(CreateNotificationCommand command);
    }
}
