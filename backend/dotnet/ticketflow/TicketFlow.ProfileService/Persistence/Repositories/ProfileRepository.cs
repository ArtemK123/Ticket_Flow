using System.Collections.Generic;
using System.Linq;
using Dapper;
using TicketFlow.Common.Providers;
using TicketFlow.Common.Repositories;
using TicketFlow.ProfileService.Client.Extensibility.Entities;
using TicketFlow.ProfileService.Client.Extensibility.Models;
using TicketFlow.ProfileService.Client.Extensibility.Serializers;

namespace TicketFlow.ProfileService.Persistence.Repositories
{
    internal class ProfileRepository : SerializingCrudRepositoryBase<int, IProfile, ProfileSerializationModel>, IProfileRepository
    {
        public ProfileRepository(IPostgresDbConnectionProvider dbConnectionProvider, IProfileSerializer profileSerializer)
            : base(dbConnectionProvider, profileSerializer)
        {
        }

        protected override string TableName => "profiles";

        protected override KeyValuePair<string, string> PrimaryKeyMapping => new KeyValuePair<string, string>("id", "Id");

        protected override IReadOnlyDictionary<string, string> NonPrimaryColumnsMapping => new Dictionary<string, string>
        {
            { "birthday", "Birthday" },
            { "phone_number", "PhoneNumber" },
            { "user_email", "UserEmail" }
        };

        public bool TryGetByUserEmail(string email, out IProfile profile)
        {
            var sql = $"SELECT {GetSelectSqlMapping()} FROM {TableName} WHERE user_email=@UserEmail;";
            profile = default;
            using var dbConnection = DbConnectionProvider.Get();
            ProfileSerializationModel databaseModel = dbConnection.Query<ProfileSerializationModel>(sql, new { UserEmail = email }).SingleOrDefault();
            if (databaseModel == null)
            {
                return false;
            }

            profile = Convert(databaseModel);
            return true;
        }
    }
}