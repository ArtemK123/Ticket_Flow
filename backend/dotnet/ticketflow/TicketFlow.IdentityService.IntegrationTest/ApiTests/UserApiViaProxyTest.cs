using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TicketFlow.IdentityService.Client.Entities;
using TicketFlow.IdentityService.Client.Extensibility.DependencyInjection;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Exceptions;
using TicketFlow.IdentityService.Client.Extensibility.Models;
using TicketFlow.IdentityService.Client.Extensibility.Proxies;
using TicketFlow.IdentityService.IntegrationTest.Fakes;
using TicketFlow.IdentityService.Persistence;
using Xunit;

namespace TicketFlow.IdentityService.IntegrationTest.ApiTests
{
    public class UserApiViaProxyTest : IDisposable
    {
        private const string UserEmail = "test@gmail.com";
        private const string AnotherUserEmail = "something@gmail.com";
        private const string Password = "AA-FF-11-22";

        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly IUserApiProxy proxy;

        public UserApiViaProxyTest()
        {
            webApplicationFactory = SetupWebApplicationFactory();
            proxy = webApplicationFactory.Services.GetService<IUserApiProxy>();
        }

        public void Dispose()
        {
            webApplicationFactory?.Dispose();
        }

        [Fact]
        public async Task Register_ThenGetByEmail_ShouldReturnUserWithGivenEmail()
        {
            RegisterRequest expectedUserRegisterRequest = new RegisterRequest(UserEmail, Password);
            RegisterRequest anotherUserRegisterRequest = new RegisterRequest(AnotherUserEmail, string.Empty);
            await proxy.RegisterAsync(expectedUserRegisterRequest);
            await proxy.RegisterAsync(anotherUserRegisterRequest);

            IUser user = await proxy.GetByEmailAsync(expectedUserRegisterRequest.Email);

            Assert.True(ValidUser(user, expectedUserRegisterRequest));
        }

        [Fact]
        public async Task Login_ValidPassword_ShouldReturnSessionToken()
        {
            RegisterRequest registerRequest = new RegisterRequest(UserEmail, Password);
            await proxy.RegisterAsync(registerRequest);

            string token = await proxy.LoginAsync(new LoginRequest(UserEmail, Password));

            Assert.NotEmpty(token);
        }

        [Fact]
        public async Task Login_InvalidPassword_ShouldThrowWrongPasswordException()
        {
            RegisterRequest registerRequest = new RegisterRequest(UserEmail, Password);
            await proxy.RegisterAsync(registerRequest);

            WrongPasswordException exception = await Assert.ThrowsAsync<WrongPasswordException>(async () => await proxy.LoginAsync(new LoginRequest(UserEmail, string.Empty)));
            Assert.Equal($"Wrong password for user with email=${UserEmail}", exception.Message);
        }

        [Fact]
        public async Task Login_ThenSecondLogin_ThenGetUserByToken_ShouldReturnAnotherTokenAndUpdateUser()
        {
            RegisterRequest registerRequest = new RegisterRequest(UserEmail, Password);
            await proxy.RegisterAsync(registerRequest);

            string firstToken = await proxy.LoginAsync(new LoginRequest(UserEmail, Password));
            await Task.Delay(1001); // jwt generator uses DateTime.Now(), so a time difference between calls should be more than 1s
            string secondToken = await proxy.LoginAsync(new LoginRequest(UserEmail, Password));

            Assert.NotEqual(firstToken, secondToken);
            await Assert.ThrowsAsync<UserNotFoundByTokenException>(async () => await proxy.GetByTokenAsync(firstToken));

            IAuthorizedUser secondTokenRequestResult = await proxy.GetByTokenAsync(secondToken);
            Assert.True(ValidUser(secondTokenRequestResult, registerRequest));
        }

        [Fact]
        public async Task Register_Success_ShouldRegisterUserAndReturnMessage()
        {
            RegisterRequest registerRequest = new RegisterRequest(UserEmail, Password);

            string result = await proxy.RegisterAsync(registerRequest);

            Assert.Equal($"Registered successfully user with email={registerRequest.Email}", result);
        }

        [Fact]
        public async Task Register_NotUniqueUserEmail_ShouldThrowUserNotUniqueException()
        {
            RegisterRequest registerRequest = new RegisterRequest(UserEmail, Password);
            await proxy.RegisterAsync(registerRequest);

            UserNotUniqueException exception = await Assert.ThrowsAsync<UserNotUniqueException>(async () => await proxy.RegisterAsync(registerRequest));
            Assert.Equal($"User with email={registerRequest.Email} already exists", exception.Message);
        }

        [Fact]
        public async Task GetByToken_Found_ShouldReturnAuthorizedUserWithGivenToken()
        {
            RegisterRequest registerRequest = new RegisterRequest(UserEmail, Password);
            await proxy.RegisterAsync(registerRequest);
            string token = await proxy.LoginAsync(new LoginRequest(UserEmail, Password));

            IAuthorizedUser authorizedUser = await proxy.GetByTokenAsync(token);

            Assert.True(ValidUser(authorizedUser, registerRequest));
        }

        [Fact]
        public async Task GetByToken_NotFound_ShouldThrowUserNotFoundByTokenException()
        {
            const string token = "jwt";

            UserNotFoundByTokenException exception = await Assert.ThrowsAsync<UserNotFoundByTokenException>(async () => await proxy.GetByTokenAsync(token));
            Assert.Equal($"User with token={token} is not found", exception.Message);
        }

        [Fact]
        public async Task Logout_ValidToken_ShouldUpdateUserRecord()
        {
            RegisterRequest registerRequest = new RegisterRequest(UserEmail, Password);
            await proxy.RegisterAsync(registerRequest);
            string token = await proxy.LoginAsync(new LoginRequest(UserEmail, Password));

            string responseFromProxy = await proxy.LogoutAsync(token);

            Assert.Equal("Logout successful", responseFromProxy);
            await Assert.ThrowsAsync<UserNotFoundByTokenException>(async () => await proxy.GetByTokenAsync(token));
        }

        [Fact]
        public async Task Logout_InvalidToken_ShouldThrowUserNotFoundByTokenException()
        {
            const string token = "jwt";

            UserNotFoundByTokenException exception = await Assert.ThrowsAsync<UserNotFoundByTokenException>(async () => await proxy.LogoutAsync(token));
            Assert.Equal($"User with token={token} is not found", exception.Message);
        }

        private static bool ValidUser(IUser user, RegisterRequest registerRequest)
            => user.Email == registerRequest.Email
               && user.Password == registerRequest.Password
               && user.Role == Role.User;

        private static WebApplicationFactory<Startup> SetupWebApplicationFactory()
        {
            HttpClient client = default;

            WebApplicationFactory<Startup> localWebApplicationFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Test");
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddSingleton(serviceProvider => Substitute.For<IMigrationRunner>());
                        services.AddSingleton<IUserRepository, FakeUserRepository>();
                        services.AddSingleton(serviceProvider => new Lazy<IHttpClientFactory>(() =>
                        {
                            var mock = Substitute.For<IHttpClientFactory>();

                            // ReSharper disable once AccessToModifiedClosure - It is expected closure, which is used for testing service proxy
                            mock.CreateClient(default).ReturnsForAnyArgs(client);
                            return mock;
                        }));
                        services.AddIdentityService();
                    });
                });

            client = localWebApplicationFactory.CreateClient();

            return localWebApplicationFactory;
        }
    }
}