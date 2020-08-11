using System.Collections.Generic;
using System.Linq;
using Dapper;
using TicketFlow.Common.Providers;
using TicketFlow.Common.Repositories;
using TicketFlow.ProfileService.Domain.Entities;
using TicketFlow.ProfileService.Domain.Models;
using TicketFlow.ProfileService.Service.Serializers;

namespace TicketFlow.ProfileService.Persistence.Repositories
{
    internal class ProfileRepository : MappedCrudRepositoryBase<int, IProfile, ProfileSerializationModel>, IProfileRepository
    {
        private readonly IProfileSerializer profileSerializer;

        public ProfileRepository(IPostgresDbConnectionProvider dbConnectionProvider, IProfileSerializer profileSerializer)
            : base(dbConnectionProvider)
        {
            this.profileSerializer = profileSerializer;
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

        protected override IProfile Convert(ProfileSerializationModel databaseModel) => profileSerializer.Create(databaseModel);

        protected override ProfileSerializationModel Convert(IProfile entity) => profileSerializer.Serialize(entity);
    }
}