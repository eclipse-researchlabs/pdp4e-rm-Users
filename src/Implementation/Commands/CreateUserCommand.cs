using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Core.Users.Implementation.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string AccountId { get; set; }
        public string Email { get; set; }



    }
}
