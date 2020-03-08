using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Core.Database.Tables;
using Core.Users.Implementation.Commands;
using Core.Users.Implementation.Helpers;
using Core.Users.Implementation.Services;
using Core.Users.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Users
{
    public class Config
    {
        public static void InitializeServices(ref IServiceCollection services)
        {
            // Services
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<INotificationService, NotificationService>();

            // Queries

            services.AddScoped<IEmailHelper, EmailHelper>();
        }
    }

    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<CreateUserCommand, User>();
        }
    }
}
