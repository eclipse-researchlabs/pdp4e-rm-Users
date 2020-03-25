using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Database;
using Core.Database.Enums;
using Core.Users.Implementation.Commands.Notifications;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Users.Implementation.CommandHandlers.Notifications
{
    public class MarkAsReadCommandHandler : IRequestHandler<MarkAsReadCommand, bool>
    {
        private IBeawreContext _beawreContext;

        public MarkAsReadCommandHandler(IBeawreContext beawreContext)
        {
            _beawreContext = beawreContext;
        }

        public Task<bool> Handle(MarkAsReadCommand request, CancellationToken cancellationToken)
        {
            var list = _beawreContext.Relationship.Where(x =>
                x.FromType == ObjectType.User && x.ToType == ObjectType.Notification && x.FromId == request.UserId);
            foreach (var item in list)
            {
                var payload = JObject.Parse(item.Payload ?? "{}");
                if(payload.ContainsKey("IsRead")) payload.SelectToken("IsRead").Replace(true);
                if(!payload.ContainsKey("IsRead")) payload.Add("IsRead", true);
                item.Payload = JsonConvert.SerializeObject(payload);
            }
            _beawreContext.SaveChanges();
            return Task.FromResult(true);
        }
    }
}
