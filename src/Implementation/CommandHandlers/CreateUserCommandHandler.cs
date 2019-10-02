using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Database;
using Core.Database.Tables;
using Core.Users.Implementation.Commands;
using MediatR;

namespace Core.Users.Implementation.CommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private IBeawreContext _beawreContext;
        private IMapper _mapper;

        public CreateUserCommandHandler(IBeawreContext beawreContext, IMapper mapper)
        {
            _beawreContext = beawreContext;
            _mapper = mapper;
        }

        public Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entry = _mapper.Map<User>(request);

            _beawreContext.User.Add(entry);
            _beawreContext.SaveChanges();

            return Task.FromResult(entry.Id);
        }
    }
}
