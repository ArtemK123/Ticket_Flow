using System.Linq;
using Dapper;
using TicketFlow.Common.Providers;
using TicketFlow.ProfileService.Models;
using TicketFlow.ProfileService.Models.Exceptions;

namespace TicketFlow.ProfileService.Domain.Repositories
{
    internal class ProfileRepository : IProfileRepository
    {
        private const string ProfileSelectMapping = "id AS Id, birthday AS Birthday, phone_number AS PhoneNumber, user_email AS UserEmail";
        private const string TableName = "profiles";

        private readonly IPostgresDbConnectionProvider dbConnectionProvider;
        private readonly IRandomValueProvider randomValueProvider;

        public ProfileRepository(IPostgresDbConnectionProvider dbConnectionProvider, IRandomValueProvider randomValueProvider)
        {
            this.dbConnectionProvider = dbConnectionProvider;
            this.randomValueProvider = randomValueProvider;
        }

        public Profile GetById(int id)
        {
            var sql = $"SELECT {ProfileSelectMapping} FROM {TableName} WHERE Id = @id;";
            using var dbConnection = dbConnectionProvider.Get();
            var profile = dbConnection.Query<Profile>(sql, new { id }).FirstOrDefault();
            if (profile == null)
            {
                throw new NotFoundException($"Profile with id={id} is not found");
            }

            return profile;
        }

        public Profile GetByUserEmail(string email)
        {
            var sql = $"SELECT {ProfileSelectMapping} FROM {TableName} WHERE user_email = @email;";
            using var dbConnection = dbConnectionProvider.Get();
            var profile = dbConnection.Query<Profile>(sql, new { email }).FirstOrDefault();
            if (profile == null)
            {
                throw new NotFoundException($"Profile with for user with email={email} is not found");
            }

            return profile;
        }

        public Profile Add(Profile profile)
        {
            int entityId = randomValueProvider.GetRandomInt(0, int.MaxValue);
            Profile insertedEntity = new Profile(entityId, profile.UserEmail, profile.PhoneNumber, profile.Birthday);
            using var dbConnection = dbConnectionProvider.Get();
            var sql = $"INSERT INTO {TableName} (id, birthday, phone_number, user_email) VALUES (@Id, @Birthday, @PhoneNumber, @UserEmail);";
            dbConnection.Execute(sql, insertedEntity);

            return insertedEntity;
        }
    }
}