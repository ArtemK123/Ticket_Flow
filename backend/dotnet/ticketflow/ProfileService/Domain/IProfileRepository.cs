using ProfileService.Models;

namespace ProfileService.Domain
{
    internal interface IProfileRepository
    {
        Profile GetById(int id);

        Profile GetByUserEmail(string email);

        int Add(Profile profile);
    }
}