using TicketFlow.Common.Factories;
using TicketFlow.IdentityService.Domain.Entities;
using TicketFlow.IdentityService.Domain.Exceptions;
using TicketFlow.IdentityService.Domain.Models;
using TicketFlow.IdentityService.Persistence;
using TicketFlow.IdentityService.WebApi.ClientModels.Requests;

namespace TicketFlow.IdentityService.Service
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IEntityFactory<IUser, UserCreationModel> userFactory;

        public UserService(IUserRepository userRepository, IEntityFactory<IUser, UserCreationModel> userFactory)
        {
            this.userRepository = userRepository;
            this.userFactory = userFactory;
        }

        public IAuthorizedUser GetByToken(string token)
        {
            if (userRepository.TryGetByToken(token, out IAuthorizedUser user))
            {
                return user;
            }

            throw new NotFoundException($"User with token={token} is not found");
        }

        public IUser GetByEmail(string email)
        {
            if (userRepository.TryGetByEmail(email, out IUser user))
            {
                return user;
            }

            throw new NotFoundException($"User with email={email} is not found");
        }

        public string Login(LoginRequest loginRequest)
        {
            IUser user = GetByEmail(loginRequest.Email);

            if (!user.TryAuthorize(loginRequest.Password, out IAuthorizedUser authorizedUser))
            {
                throw new WrongPasswordException($"Wrong password for user with email=${loginRequest.Email}");
            }

            userRepository.Update(authorizedUser);
            return authorizedUser.Token;
        }

        public void Register(RegisterRequest registerRequest)
        {
            if (userRepository.TryGetByEmail(registerRequest.Email, out _))
            {
                throw new NotUniqueEntityException($"User with email={registerRequest.Email} already exists");
            }

            IUser user = userFactory.Create(new UserCreationModel(registerRequest.Email, registerRequest.Password, Role.User));
            userRepository.Add(user);
        }

        public void Logout(string token)
        {
            IUser user = GetByToken(token);
            userRepository.Update(user);
        }
    }
}