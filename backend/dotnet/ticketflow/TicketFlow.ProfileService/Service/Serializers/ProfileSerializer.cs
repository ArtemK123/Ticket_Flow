using TicketFlow.ProfileService.Domain.Entities;
using TicketFlow.ProfileService.Domain.Models;

namespace TicketFlow.ProfileService.Service.Serializers
{
    internal class ProfileSerializer : IProfileSerializer
    {
        public IProfile Create(ProfileSerializationModel creationModel)
            => new Profile(creationModel.Id, creationModel.UserEmail, creationModel.PhoneNumber, creationModel.Birthday);

        public ProfileSerializationModel Serialize(IProfile profile)
            => new ProfileSerializationModel { Id = profile.Id, UserEmail = profile.UserEmail, PhoneNumber = profile.PhoneNumber, Birthday = profile.Birthday };
    }
}