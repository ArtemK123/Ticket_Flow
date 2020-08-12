using TicketFlow.Common.Providers;
using TicketFlow.ProfileService.Domain.Entities;
using TicketFlow.ProfileService.Domain.Models;

namespace TicketFlow.ProfileService.Service.Factories
{
    internal class ProfileFactory : IProfileFactory
    {
        private readonly IRandomValueProvider randomValueProvider;

        public ProfileFactory(IRandomValueProvider randomValueProvider)
        {
            this.randomValueProvider = randomValueProvider;
        }

        public IProfile Create(ProfileCreationModel creationModel)
        {
            var newProfileId = randomValueProvider.GetRandomInt(0, int.MaxValue);
            return new Profile(newProfileId, creationModel.UserEmail, creationModel.PhoneNumber, creationModel.Birthday);
        }

        public IProfile Create(StoredProfileCreationModel creationModel)
        {
            return new Profile(creationModel.Id, creationModel.UserEmail, creationModel.PhoneNumber, creationModel.Birthday);
        }
    }
}