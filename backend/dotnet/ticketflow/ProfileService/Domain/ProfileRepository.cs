using ProfileService.Models;

namespace ProfileService.Domain
{
    internal class ProfileRepository : IProfileRepository
    {
        public Profile GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Profile GetByUserEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public int Add(Profile profile)
        {
            throw new System.NotImplementedException();
        }
    }
}