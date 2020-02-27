using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Database;
using Core.Users.Implementation.Commands;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Users.Implementation.CommandHandlers
{
    public class UpdateNotificationsSettingsCommandHandler : IRequestHandler<UpdateNotificationsSettingsCommand, bool>
    {
        private IBeawreContext _beawreContext;

        public UpdateNotificationsSettingsCommandHandler(IBeawreContext beawreContext)
        {
            _beawreContext = beawreContext;
        }

        public Task<bool> Handle(UpdateNotificationsSettingsCommand request, CancellationToken cancellationToken)
        {
            var user = _beawreContext.User.FirstOrDefault(x => x.Id == request.UserId);
            if(user == null) throw new Exception("User not found!");

            var payload = JObject.Parse(string.IsNullOrEmpty(user.Payload) ? "{}" : user.Payload);
            if(payload.ContainsKey("notificationsSettings"))
                payload.SelectToken("notificationsSettings").Replace(JObject.Parse(request.Settings.ToString()));
            else
                payload.Add(new JProperty("notificationsSettings", JObject.Parse(request.Settings.ToString())));

            user.Payload = JsonConvert.SerializeObject(payload);
            _beawreContext.SaveChanges();
            return Task.FromResult(true);
        }
    }
}
