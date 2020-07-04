using System.Linq;
using Dapper;
using ProfileService.Models;
using ProfileService.Models.Exceptions;

namespace ProfileService.Domain.Repositories
{
    internal class ProfileRepository : IProfileRepository
    {
        private readonly IDbConnectionProvider dbConnectionProvider;

        public ProfileRepository(IDbConnectionProvider dbConnectionProvider)
        {
            this.dbConnectionProvider = dbConnectionProvider;
        }

        public Profile GetById(int id)
        {
            using var dbConnection = dbConnectionProvider.Get();
            const string sql = "SELECT * FROM profiles WHERE Id = @id";
            var profile = dbConnection.Query<Profile>(sql, new { id }).FirstOrDefault();
            if (profile == null)
            {
                throw new NotFoundException($"Profile with id={id} is not found");
            }

            return profile;
        }

        public Profile GetByUserEmail(string email)
        {
            using var dbConnection = dbConnectionProvider.Get();
            const string sql = "SELECT * FROM profiles WHERE user_email = @email";
            var profile = dbConnection.Query<Profile>(sql, new { email }).FirstOrDefault();
            if (profile == null)
            {
                throw new NotFoundException($"Profile with for user with email={email} is not found");
            }

            return profile;
        }

        public int Add(Profile profile)
        {
            using var dbConnection = dbConnectionProvider.Get();
            const string sql = "INSERT INTO profiles (birthday, phone_number, user_email) VALUES (@birthday, @phone_number, @user_email) RETURNING id";
            int insertedRecordId = dbConnection.Query<int>(sql, new { profile.Birthday, profile.PhoneNumber, profile.UserEmail }).FirstOrDefault();

            return insertedRecordId;
        }
    }
}