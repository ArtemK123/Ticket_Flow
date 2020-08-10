using System.Collections.Generic;
using System.Linq;
using Dapper;
using TicketFlow.Common.Providers;
using TicketFlow.Common.Repositories;
using TicketFlow.IdentityService.Domain.Entities;
using TicketFlow.IdentityService.Domain.Models;
using TicketFlow.IdentityService.Persistence.EntityModels;
using TicketFlow.IdentityService.Service.Factories;

namespace TicketFlow.IdentityService.Persistence
{
    internal class UserRepository : MappedCrudRepositoryBase<string, IUser, UserDatabaseModel>, IUserRepository
    {
        private readonly IUserFactory userFactory;

        public UserRepository(IPostgresDbConnectionProvider dbConnectionProvider, IUserFactory userFactory)
            : base(dbConnectionProvider)
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
            UserDatabaseModel databaseModel = dbConnection.Query<UserDatabaseModel>(sql, new { Token = token }).SingleOrDefault();
            if (databaseModel == null)
            {
                return false;
            }

            authorizedUser = userFactory.Create(new AuthorizedUserCreationModel(databaseModel.Email, databaseModel.Password, (Role)databaseModel.Role, databaseModel.Token));
            return true;
        }

        protected override IUser Convert(UserDatabaseModel databaseModel)
        {
            var creationModel = string.IsNullOrEmpty(databaseModel.Token)
                ? new UserCreationModel(databaseModel.Email, databaseModel.Password, (Role)databaseModel.Role)
                : new AuthorizedUserCreationModel(databaseModel.Email, databaseModel.Password, (Role)databaseModel.Role, databaseModel.Token);

            return userFactory.Create(creationModel);
        }

        protected override UserDatabaseModel Convert(IUser entity)
        {
            return entity is IAuthorizedUser authorizedUser
                ? new UserDatabaseModel { Email = authorizedUser.Email, Password = authorizedUser.Password, Role = (int)authorizedUser.Role, Token = authorizedUser.Token }
                : new UserDatabaseModel { Email = entity.Email, Password = entity.Password, Role = (int)entity.Role, Token = null };
        }

        protected override object GetSearchByIdentifierParams(string identifier) => new { Email = identifier };
    }
}