using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Database.Tables;
using Core.Users.Implementation.Commands;

namespace Core.Users.Interfaces.Services
{
    public interface IUserService
    {
        Task<Guid> Create(CreateUserCommand command);
        User Get(Expression<Func<User, bool>> func);
        bool UpdateNotificationsSettings(UpdateNotificationsSettingsCommand command);
    }
}
