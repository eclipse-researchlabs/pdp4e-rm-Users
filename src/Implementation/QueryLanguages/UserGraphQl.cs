using System;
using System.Collections.Generic;
using System.Text;
using Core.Database;
using Core.Database.Tables;
using GraphQL.EntityFramework;

namespace Core.Users.Implementation.QueryLanguages
{
    public class UserGraphQl : EfObjectGraphType<BeawreContext, User>
    {
        public UserGraphQl(IEfGraphQLService<BeawreContext> graphQlService) : base(graphQlService)
        {
            Field(x => x.Id);
            Field(x => x.Username);
            Field(x => x.IsDeleted);
            Field(x => x.CreatedDateTime);

        }
    }
}
