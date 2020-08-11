using System.Collections.Generic;
using System.Linq;
using Dapper;
using TicketFlow.Common.Providers;
using TicketFlow.Common.Repositories;
using TicketFlow.IdentityService.Domain.Entities;
using TicketFlow.IdentityService.Domain.Models;
using TicketFlow.IdentityService.Service.Factories;
using TicketFlow.IdentityService.Service.Serializers;

namespace TicketFlow.IdentityService.Persistence
{
    internal class UserRepository : MappedCrudRepositoryBase<string, IUser, UserSerializationModel>, IUserRepository
    {
        private readonly IUserSerializer userSerializer;
        private readonly IUserFactory userFactory;

        public UserRepository(IPostgresDbConnectionProvider dbConnectionProvider, IUserSerializer userSerializer, IUserFactory userFactory)
            : base(dbConnectionProvider)
        {
            this.userSerializer = userSerializer;
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

        protected override IUser Convert(UserSerializationModel databaseModel) => userSerializer.Deserialize(databaseModel);

        protected override UserSerializationModel Convert(IUser entity) => userSerializer.Serialize(entity);

        protected override object GetSearchByIdentifierParams(string identifier) => new { Email = identifier };
    }
}