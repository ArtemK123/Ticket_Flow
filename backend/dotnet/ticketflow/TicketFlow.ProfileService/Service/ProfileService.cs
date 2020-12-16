using TicketFlow.ProfileService.Client.Extensibility.Entities;
using TicketFlow.ProfileService.Client.Extensibility.Exceptions;
using TicketFlow.ProfileService.Persistence.Repositories;

namespace TicketFlow.ProfileService.Service
{
    internal class ProfileService : IProfileService
    {
        private readonly IProfileRepository profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        public IProfile GetById(int id)
            => profileRepository.TryGet(id, out IProfile profile)
                ? profile
                : throw new ProfileNotFoundByIdException($"Profile with id={id} is not found");

        public IProfile GetByUserEmail(string email)
            => profileRepository.TryGetByUserEmail(email, out IProfile profile)
                ? profile
                : throw new ProfileNotFoundByUserEmailException($"Profile for user with email={email} is not found");

        public void Add(IProfile profile)
        {
            if (profileRepository.TryGetByUserEmail(profile.UserEmail, out IProfile _))
            {
                throw new NotUniqueUserProfileException($"Profile for user with email={profile.UserEmail} already exists");
            }

            profileRepository.Add(profile);
        }
    }
}