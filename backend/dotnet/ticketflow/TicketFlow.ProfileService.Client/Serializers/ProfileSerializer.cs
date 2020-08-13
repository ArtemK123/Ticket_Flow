using TicketFlow.ProfileService.Client.Entities;
using TicketFlow.ProfileService.Client.Extensibility.Entities;
using TicketFlow.ProfileService.Client.Extensibility.Factories;
using TicketFlow.ProfileService.Client.Extensibility.Models;
using TicketFlow.ProfileService.Client.Extensibility.Serializers;

namespace TicketFlow.ProfileService.Client.Serializers
{
    internal class ProfileSerializer : IProfileSerializer
    {
        private readonly IProfileFactory profileFactory;

        public ProfileSerializer(IProfileFactory profileFactory)
        {
            this.profileFactory = profileFactory;
        }

        public IProfile Create(ProfileSerializationModel creationModel)
            => new Profile(creationModel.Id, creationModel.UserEmail, creationModel.PhoneNumber, creationModel.Birthday);

        public ProfileSerializationModel Serialize(IProfile profile)
            => new ProfileSerializationModel { Id = profile.Id, UserEmail = profile.UserEmail, PhoneNumber = profile.PhoneNumber, Birthday = profile.Birthday };

        public IProfile Deserialize(ProfileSerializationModel serializationModel)
            => profileFactory.Create(new StoredProfileCreationModel(serializationModel.Id, serializationModel.UserEmail, serializationModel.PhoneNumber, serializationModel.Birthday));
    }
}