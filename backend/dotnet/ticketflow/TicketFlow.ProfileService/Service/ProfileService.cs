using TicketFlow.ProfileService.Domain.Entities;
using TicketFlow.ProfileService.Domain.Exceptions;
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
            => profileRepository.TryGet(id, out IProfile profile) ? profile : throw new NotFoundException($"Profile with id={id} is not found");

        public IProfile GetByUserEmail(string email)
            => profileRepository.TryGetByUserEmail(email, out IProfile profile) ? profile : throw new NotFoundException($"Profile for user with email={email} is not found");

        public void Add(IProfile profile)
        {
            if (profileRepository.TryGetByUserEmail(profile.UserEmail, out IProfile _))
            {
                throw new NotUniqueEntityException($"Profile for user with email={profile.UserEmail} already exists");
            }

            profileRepository.Add(profile);
        }
    }
}