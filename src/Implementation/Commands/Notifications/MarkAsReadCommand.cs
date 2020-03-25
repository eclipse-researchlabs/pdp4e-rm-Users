using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Core.Users.Implementation.Commands.Notifications
{
    public class MarkAsReadCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
    }
}
