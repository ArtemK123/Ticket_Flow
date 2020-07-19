using ProfileService.Domain.Repositories;
using ProfileService.Models;

namespace ProfileService.Service
{
    internal class ProfileService : IProfileService
    {
        private readonly IProfileRepository profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        public Profile GetById(int id)
        {
            return profileRepository.GetById(id);
        }

        public Profile GetByUserEmail(string email)
        {
            return profileRepository.GetByUserEmail(email);
        }

        public Profile Add(Profile profile)
        {
            return profileRepository.Add(profile);
        }
    }
}