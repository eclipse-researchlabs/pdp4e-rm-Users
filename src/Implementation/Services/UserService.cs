using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Database;
using Core.Database.Tables;
using Core.Users.Implementation.Commands;
using Core.Users.Interfaces.Services;
using MediatR;

namespace Core.Users.Implementation.Services
{
    public class UserService : IUserService
    {
        private IMediator _mediator;
        private IBeawreContext _beawreContext;

        public UserService(IMediator mediator, IBeawreContext beawreContext)
        {
            _mediator = mediator;
            _beawreContext = beawreContext;
        }

        public async Task<Guid> Create(CreateUserCommand command) => await _mediator.Send(command);

        public User Get(Expression<Func<User, bool>> func) => _beawreContext.User.FirstOrDefault(func);
    }

}
