using System.Collections.Generic;
using TicketFlow.Common.Providers;
using TicketFlow.Common.Repositories;
using TicketFlow.IdentityService.Domain.Entities;
using TicketFlow.IdentityService.Persistence.EntityModels;

namespace TicketFlow.IdentityService.Persistence
{
    internal class UserRepository : MappedCrudRepositoryBase<string, IUser, UserDatabaseModel>, IUserRepository
    {
        public UserRepository(IDbConnectionProvider dbConnectionProvider)
            : base(dbConnectionProvider)
        {
        }

        protected override string TableName => "users";

        protected override KeyValuePair<string, string> PrimaryKeyMapping => new KeyValuePair<string, string>("email", "Email");

        protected override IReadOnlyDictionary<string, string> NonPrimaryColumnsMapping => new Dictionary<string, string>
        {
            { "password", "Password" },
            { "role", "Role" },
            { "token", "Token" },
        };

        public bool TryGetByToken(string token, out IAuthorizedUser authorizedUser)
        {
            throw new System.NotImplementedException();
        }

        protected override IUser Convert(UserDatabaseModel databaseModel)
        {
            throw new System.NotImplementedException();
        }

        protected override UserDatabaseModel Convert(IUser entity)
        {
            throw new System.NotImplementedException();
        }
    }
}