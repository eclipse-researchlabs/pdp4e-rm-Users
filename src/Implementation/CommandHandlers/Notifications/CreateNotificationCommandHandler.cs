using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Tables;
using Core.Users.Implementation.Commands.Notifications;
using Core.Users.Implementation.Helpers;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Users.Implementation.CommandHandlers.Notifications
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, bool>
    {
        private IBeawreContext _beawreContext;
        private IEmailHelper _emailHelper;

        public CreateNotificationCommandHandler(IBeawreContext beawreContext, IEmailHelper emailHelper)
        {
            _beawreContext = beawreContext;
            _emailHelper = emailHelper;
        }

        public Task<bool> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var users = _beawreContext.User.Where(x => request.UserId.Contains(x.Id)).ToList();
            foreach(var user in users)
            {
                var payload = JObject.Parse(string.IsNullOrEmpty(user.Payload) ? "{}" : user.Payload);
                if (payload.ContainsKey("notificationsSettings"))
                {
                    var settings = payload.SelectToken("notificationsSettings")
                        .SelectToken(request.Payload.StringValue1).ToObject<JObject[]>().FirstOrDefault(x => x.SelectToken("key").Value<string>() == request.Payload.Type).Values<JProperty>().ToList();
                    bool sendEmail = settings.FirstOrDefault(x => x.Name == "email").Value.Value<bool>();
                    bool sendWebApp = settings.FirstOrDefault(x => x.Name == "webapp").Value.Value<bool>();

                    if (sendWebApp) {
                        _beawreContext.Relationship.Add(new Relationship() { FromType = ObjectType.User, ToType = ObjectType.Notification,  FromId = user.Id, ToId = Guid.Empty, Payload = JsonConvert.SerializeObject(request.Payload) });
                    }

                    if (sendEmail && !string.IsNullOrEmpty(request.Payload.Title) && !string.IsNullOrEmpty(request.Payload.Content)) {
                        _emailHelper.Send(user.Email, user.Username, request.Payload.Title, request.Payload.Content);
                    }
                }
            }
            _beawreContext.SaveChanges();
            return Task.FromResult(true);
        }
    }
}
