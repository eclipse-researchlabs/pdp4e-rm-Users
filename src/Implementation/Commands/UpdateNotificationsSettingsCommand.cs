using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Core.Users.Implementation.Commands
{
    public class UpdateNotificationsSettingsCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Object Settings { get; set; }
    }
}
