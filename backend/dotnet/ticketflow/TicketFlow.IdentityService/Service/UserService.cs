using TicketFlow.IdentityService.Entities;
using TicketFlow.IdentityService.Entities.Exceptions;
using TicketFlow.IdentityService.Persistence;
using TicketFlow.IdentityService.WebApi.ClientModels.Requests;

namespace TicketFlow.IdentityService.Service
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IJwtGenerator jwtGenerator;

         public UserService(IUserRepository userRepository, IJwtGenerator jwtGenerator)
         {
             this.userRepository = userRepository;
             this.jwtGenerator = jwtGenerator;
         }

        public User GetByToken(string token)
        {
            if (userRepository.TryGetByToken(token, out User user))
            {
                return user;
            }

            throw new NotFoundException($"User with token={token} is not found");
        }

        public User GetByEmail(string email)
        {
            if (userRepository.TryGetByEmail(email, out User user))
            {
                return user;
            }

            throw new NotFoundException($"User with email={email} is not found");
        }

        public string Login(LoginRequest loginRequest)
        {
            User user = GetByEmail(loginRequest.Email);

            if (!user.Password.Equals(loginRequest.Password))
            {
                throw new WrongPasswordException($"Wrong password for user with email={loginRequest.Email}");
            }

            string newJwtToken = jwtGenerator.Generate(user);
            user.Token = newJwtToken;
            userRepository.Update(user);
            return newJwtToken;
        }

        public void Register(RegisterRequest registerRequest)
        {
            if (userRepository.TryGetByEmail(registerRequest.Email, out _))
            {
                throw new NotUniqueEntityException($"User with email={registerRequest.Email} already exists");
            }

            User user = new User(registerRequest.Email, registerRequest.Password, Role.User, null);

            userRepository.Add(user);
        }

        public void Logout(string token)
        {
            User user = GetByToken(token);
            user.Token = null;
            userRepository.Update(user);
        }
    }
}