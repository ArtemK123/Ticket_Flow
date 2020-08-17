using System.Collections.Generic;
using System.Linq;
using Dapper;
using TicketFlow.Common.Providers;
using TicketFlow.Common.Repositories;
using TicketFlow.IdentityService.Client.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Factories;
using TicketFlow.IdentityService.Client.Extensibility.Models;
using TicketFlow.IdentityService.Client.Extensibility.Serializers;

namespace TicketFlow.IdentityService.Persistence
{
    internal class UserRepository : SerializingCrudRepositoryBase<string, IUser, UserSerializationModel>, IUserRepository
    {
        private readonly IUserFactory userFactory;

        public UserRepository(IPostgresDbConnectionProvider dbConnectionProvider, IUserSerializer userSerializer, IUserFactory userFactory)
            : base(dbConnectionProvider, userSerializer)
        {
            this.userFactory = userFactory;
        }

        protected override string TableName => "users";

        protected override KeyValuePair<string, string> PrimaryKeyMapping => new KeyValuePair<string, string>("email", "Email");

        protected override IReadOnlyDictionary<string, string> NonPrimaryColumnsMapping => new Dictionary<string, string>
        {
            { "password", "Password" },
            { "role", "Role" },
            { "token", "Token" }
        };

        public bool TryGetByToken(string token, out IAuthorizedUser authorizedUser)
        {
            authorizedUser = default;

            var sql = $"SELECT {GetSelectSqlMapping()} FROM {TableName} WHERE token=@Token;";

            using var dbConnection = DbConnectionProvider.Get();
            UserSerializationModel databaseModel = dbConnection.Query<UserSerializationModel>(sql, new { Token = token }).SingleOrDefault();
            if (databaseModel == null)
            {
                return false;
            }

            authorizedUser = userFactory.Create(new AuthorizedUserCreationModel(databaseModel.Email, databaseModel.Password, (Role)databaseModel.Role, databaseModel.Token));
            return true;
        }

        protected override object GetSearchByIdentifierParams(string identifier) => new { Email = identifier };
    }
}