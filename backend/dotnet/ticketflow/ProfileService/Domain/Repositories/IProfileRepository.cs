using ProfileService.Models;

namespace ProfileService.Domain.Repositories
{
    internal interface IProfileRepository
    {
        Profile GetById(int id);

        Profile GetByUserEmail(string email);

        Profile Add(Profile profile);
    }
}