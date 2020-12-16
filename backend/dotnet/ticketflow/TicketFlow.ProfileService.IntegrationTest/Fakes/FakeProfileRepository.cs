using System.Collections.Generic;
using System.Linq;
using TicketFlow.ProfileService.Client.Extensibility.Entities;
using TicketFlow.ProfileService.Client.Extensibility.Models;
using TicketFlow.ProfileService.Client.Extensibility.Serializers;
using TicketFlow.ProfileService.Persistence.Repositories;

namespace TicketFlow.ProfileService.IntegrationTest.Fakes
{
    internal class FakeProfileRepository : IProfileRepository
    {
        private readonly IProfileSerializer profileSerializer;
        private readonly List<ProfileSerializationModel> storedProfileModels;

        public FakeProfileRepository(IProfileSerializer profileSerializer)
        {
            this.profileSerializer = profileSerializer;
            storedProfileModels = new List<ProfileSerializationModel>();
        }

        public bool TryGet(int identifier, out IProfile entity)
        {
            ProfileSerializationModel profileModel = storedProfileModels.FirstOrDefault(model => model.Id == identifier);
            if (profileModel != null)
            {
                entity = profileSerializer.Deserialize(profileModel);
                return true;
            }

            entity = default;
            return false;
        }

        public IReadOnlyCollection<IProfile> GetAll()
            => storedProfileModels.Select(profile => profileSerializer.Deserialize(profile)).ToArray();

        public void Add(IProfile entity)
        {
            ProfileSerializationModel model = profileSerializer.Serialize(entity);
            storedProfileModels.Add(model);
        }

        public void Update(IProfile entity)
        {
            if (TryGet(entity.Id, out IProfile _))
            {
                Delete(entity.Id);
                Add(entity);
            }
        }

        public void Delete(int identifier)
        {
            ProfileSerializationModel profileModel = storedProfileModels.FirstOrDefault(model => model.Id == identifier);
            if (profileModel != null)
            {
                storedProfileModels.Remove(profileModel);
            }
        }

        public bool TryGetByUserEmail(string email, out IProfile profile)
        {
            ProfileSerializationModel profileModel = storedProfileModels.FirstOrDefault(model => model.UserEmail == email);
            if (profileModel != null)
            {
                profile = profileSerializer.Deserialize(profileModel);
                return true;
            }

            profile = default;
            return false;
        }
    }
}