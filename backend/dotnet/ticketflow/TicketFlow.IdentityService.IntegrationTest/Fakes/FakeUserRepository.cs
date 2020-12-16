using System.Collections.Generic;
using System.Linq;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Models;
using TicketFlow.IdentityService.Client.Extensibility.Serializers;
using TicketFlow.IdentityService.Persistence;

namespace TicketFlow.IdentityService.IntegrationTest.Fakes
{
    internal class FakeUserRepository : IUserRepository
    {
        private readonly IUserSerializer userSerializer;
        private readonly List<UserSerializationModel> storedUserModels;

        public FakeUserRepository(IUserSerializer userSerializer)
        {
            this.userSerializer = userSerializer;
            storedUserModels = new List<UserSerializationModel>();
        }

        public bool TryGet(string identifier, out IUser entity)
        {
            UserSerializationModel userModel = storedUserModels.FirstOrDefault(model => model.Email == identifier);
            if (userModel != null)
            {
                entity = userSerializer.Deserialize(userModel);
                return true;
            }

            entity = default;
            return false;
        }

        public IReadOnlyCollection<IUser> GetAll()
            => storedUserModels.Select(ticket => userSerializer.Deserialize(ticket)).ToArray();

        public void Add(IUser entity)
        {
            UserSerializationModel model = userSerializer.Serialize(entity);
            storedUserModels.Add(model);
        }

        public void Update(IUser entity)
        {
            if (TryGet(entity.Email, out IUser _))
            {
                Delete(entity.Email);
                Add(entity);
            }
        }

        public void Delete(string identifier)
        {
            UserSerializationModel ticketModel = storedUserModels.FirstOrDefault(model => model.Email == identifier);
            if (ticketModel != null)
            {
                storedUserModels.Remove(ticketModel);
            }
        }

        public bool TryGetByToken(string token, out IAuthorizedUser authorizedUser)
        {
            UserSerializationModel userModel = storedUserModels.FirstOrDefault(model => model.Token == token);
            if (userModel != null)
            {
                authorizedUser = userSerializer.Deserialize(userModel) as IAuthorizedUser;
                return true;
            }

            authorizedUser = default;
            return false;
        }
    }
}