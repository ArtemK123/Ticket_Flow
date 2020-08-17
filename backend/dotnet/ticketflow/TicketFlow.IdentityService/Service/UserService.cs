using TicketFlow.IdentityService.Client.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Exceptions;
using TicketFlow.IdentityService.Client.Extensibility.Factories;
using TicketFlow.IdentityService.Client.Extensibility.Models;
using TicketFlow.IdentityService.Persistence;

namespace TicketFlow.IdentityService.Service
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserFactory userFactory;

        public UserService(IUserRepository userRepository, IUserFactory userFactory)
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
            if (userRepository.TryGet(email, out IUser user))
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
            if (userRepository.TryGet(registerRequest.Email, out _))
            {
                throw new NotUniqueEntityException($"User with email={registerRequest.Email} already exists");
            }

            IUser user = userFactory.Create(new UserCreationModel(registerRequest.Email, registerRequest.Password, Role.User));
            userRepository.Add(user);
        }

        public void Logout(string token)
        {
            IUser storedUser = GetByToken(token);
            IUser loggedOutUser = userFactory.Create(new UserCreationModel(storedUser.Email, storedUser.Password, storedUser.Role));

            userRepository.Update(loggedOutUser);
        }
    }
}