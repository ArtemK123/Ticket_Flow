using TicketFlow.IdentityService.Client.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Factories;
using TicketFlow.IdentityService.Client.Extensibility.Models;
using TicketFlow.IdentityService.Client.Extensibility.Serializers;

namespace TicketFlow.IdentityService.Client.Serializers
{
    internal class UserSerializer : IUserSerializer
    {
        private readonly IUserFactory userFactory;

        public UserSerializer(IUserFactory userFactory)
        {
            this.userFactory = userFactory;
        }

        public UserSerializationModel Serialize(IUser entity)
            => new UserSerializationModel
            {
                Email = entity.Email,
                Password = entity.Password,
                Role = (int)entity.Role,
                Token = entity is IAuthorizedUser authorizedUser ? authorizedUser.Token : null
            };

        public IUser Deserialize(UserSerializationModel serializationModel)
        {
            if (!string.IsNullOrEmpty(serializationModel.Token))
            {
                return userFactory.Create(new AuthorizedUserCreationModel(serializationModel.Email, serializationModel.Password, (Role)serializationModel.Role, serializationModel.Token));
            }

            return userFactory.Create(new UserCreationModel(serializationModel.Email, serializationModel.Password, (Role)serializationModel.Role));
        }
    }
}