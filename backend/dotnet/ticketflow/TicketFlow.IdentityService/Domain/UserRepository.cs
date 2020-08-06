using System.Linq;
using Dapper;
using TicketFlow.Common.Providers;
using TicketFlow.IdentityService.Domain.EntityModels;
using TicketFlow.IdentityService.Service.Entities;

namespace TicketFlow.IdentityService.Domain
{
    internal class UserRepository : IUserRepository
    {
        private const string UserSelectMapping = "email AS Email, password AS Password, role AS Role, token as Token";
        private const string TableName = "users";

        private readonly IPostgresDbConnectionProvider dbConnectionProvider;

        public UserRepository(IPostgresDbConnectionProvider dbConnectionProvider)
        {
            this.dbConnectionProvider = dbConnectionProvider;
        }

        public bool TryGetByToken(string token, out User user)
        {
            var sql = $"SELECT {UserSelectMapping} FROM {TableName} WHERE token = @token;";
            using var dbConnection = dbConnectionProvider.Get();
            UserDatabaseModel userDatabaseModel = dbConnection.Query<UserDatabaseModel>(sql, new { token }).SingleOrDefault();
            user = Convert(userDatabaseModel);
            return userDatabaseModel != null;
        }

        public bool TryGetByEmail(string email, out User user)
        {
            var sql = $"SELECT {UserSelectMapping} FROM {TableName} WHERE email = @email;";
            using var dbConnection = dbConnectionProvider.Get();
            UserDatabaseModel userDatabaseModel = dbConnection.Query<UserDatabaseModel>(sql, new { email }).SingleOrDefault();
            user = Convert(userDatabaseModel);
            return userDatabaseModel != null;
        }

        public void Update(User user)
        {
            var sql = $"UPDATE {TableName} SET password=@Password, role=@Role, token=@Token WHERE email = @Email";
            var sqlParams = new
            {
                user.Email,
                user.Password,
                Role = user.Role.ToString(),
                user.Token
            };

            using var dbConnection = dbConnectionProvider.Get();
            dbConnection.Execute(sql, sqlParams);
        }

        public void Add(User user)
        {
            using var dbConnection = dbConnectionProvider.Get();
            var sql = $"INSERT INTO {TableName} (email, password, role, token) VALUES (@Email, @Password, @Role, @Token);";
            dbConnection.Execute(sql, user);
        }

        private static User Convert(UserDatabaseModel userDatabaseModel)
            => userDatabaseModel != null
                ? new User(userDatabaseModel.Email, userDatabaseModel.Password, userDatabaseModel.Role, userDatabaseModel.Token)
                : null;
    }
}