using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TicketFlow.Common.Exceptions;
using TicketFlow.ProfileService.Client.Extensibility.DependencyInjection;
using TicketFlow.ProfileService.Client.Extensibility.Entities;
using TicketFlow.ProfileService.Client.Extensibility.Exceptions;
using TicketFlow.ProfileService.Client.Extensibility.Models;
using TicketFlow.ProfileService.Client.Extensibility.Proxies;
using TicketFlow.ProfileService.IntegrationTest.Fakes;
using TicketFlow.ProfileService.Persistence.Repositories;
using Xunit;

namespace TicketFlow.ProfileService.IntegrationTest.ApiTests
{
    public class ProfileApiViaProxyTest : IDisposable
    {
        private const string ValidUserEmail = "test@gmail.com";
        private const string InvalidUserEmail = "not an email!";
        private const long ValidPhoneNumber = 380971234567;
        private static readonly DateTime ValidBirthday = new DateTime(2000, 1, 1);

        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly IProfileApiProxy proxy;

        public ProfileApiViaProxyTest()
        {
            webApplicationFactory = SetupWebApplicationFactory();
            proxy = webApplicationFactory.Services.GetService<IProfileApiProxy>();
        }

        public void Dispose()
        {
            webApplicationFactory?.Dispose();
        }

        [Fact]
        public async Task Add_ValidModel_ShouldAddProfileAndReturnAddedProfile()
        {
            var creationModel = new ProfileCreationModel(ValidUserEmail, ValidPhoneNumber, ValidBirthday);

            IProfile addedProfile = await proxy.AddAsync(creationModel);

            Assert.True(addedProfile.Id > 0);
            Assert.True(SameProfile(addedProfile, creationModel));
        }

        [Fact]
        public async Task Add_EmptyFieldsInModel_ShouldThrowInvalidRequestModelException()
        {
            var creationModel = new ProfileCreationModel(default, default, default);

            await Assert.ThrowsAsync<InvalidRequestModelException>(async () => await proxy.AddAsync(creationModel));
        }

        [Fact]
        public async Task Add_InvalidEmail_ShouldThrowInvalidRequestModelException()
        {
            var creationModel = new ProfileCreationModel(InvalidUserEmail, ValidPhoneNumber, ValidBirthday);

            await Assert.ThrowsAsync<InvalidRequestModelException>(async () => await proxy.AddAsync(creationModel));
        }

        [Fact]
        public async Task Add_DefaultPhoneNumber_ShouldAddProfile()
        {
            var creationModel = new ProfileCreationModel(ValidUserEmail, default, ValidBirthday);

            IProfile addedProfile = await proxy.AddAsync(creationModel);

            Assert.True(addedProfile.Id > 0);
            Assert.True(SameProfile(addedProfile, creationModel));
        }

        [Fact]
        public async Task Add_ProfileWithGivenUserEmailAlreadyExists_ShouldThrowNotUniqueUserProfileException()
        {
            var creationModel = new ProfileCreationModel(ValidUserEmail, ValidPhoneNumber, ValidBirthday);

            await proxy.AddAsync(creationModel);

            NotUniqueUserProfileException exception = await Assert.ThrowsAsync<NotUniqueUserProfileException>(async () => await proxy.AddAsync(creationModel));
            Assert.Equal($"Profile for user with email={ValidUserEmail} already exists", exception.Message);
        }

        [Fact]
        public async Task GetById_Found_ShouldReturnProfileWithGivenId()
        {
            var creationModel = new ProfileCreationModel(ValidUserEmail, ValidPhoneNumber, ValidBirthday);
            IProfile addedProfile = await proxy.AddAsync(creationModel);

            IProfile fetchedProfile = await proxy.GetByIdAsync(addedProfile.Id);

            Assert.True(fetchedProfile.Id == addedProfile.Id);
            Assert.True(SameProfile(fetchedProfile, creationModel));
        }

        [Fact]
        public async Task GetById_NotFound_ShouldThrowProfileNotFoundByIdException()
        {
            await Assert.ThrowsAsync<ProfileNotFoundByIdException>(async () => await proxy.GetByIdAsync(1));
        }

        [Fact]
        public async Task GetByUserEmail_Found_ShouldReturnProfileWithGivenUserEmail()
        {
            var creationModel = new ProfileCreationModel(ValidUserEmail, ValidPhoneNumber, ValidBirthday);
            await proxy.AddAsync(creationModel);

            IProfile fetchedProfile = await proxy.GetByUserEmailAsync(ValidUserEmail);

            Assert.True(SameProfile(fetchedProfile, creationModel));
        }

        [Fact]
        public async Task GetByUserEmail_NotFound_ShouldThrowProfileNotFoundByUserEmailException()
        {
            await Assert.ThrowsAsync<ProfileNotFoundByUserEmailException>(async () => await proxy.GetByUserEmailAsync(ValidUserEmail));
        }

        private static bool SameProfile(IProfile profile, ProfileCreationModel profileCreationModel)
            => profile.Birthday == profileCreationModel.Birthday
               && profile.PhoneNumber == profileCreationModel.PhoneNumber
               && profile.UserEmail == profileCreationModel.UserEmail;

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
                        services.AddSingleton<IProfileRepository, FakeProfileRepository>();
                        services.AddSingleton(serviceProvider => new Lazy<IHttpClientFactory>(() =>
                        {
                            var mock = Substitute.For<IHttpClientFactory>();

                            // ReSharper disable once AccessToModifiedClosure - It is expected closure, which is used for testing service proxy
                            mock.CreateClient(default).ReturnsForAnyArgs(client);
                            return mock;
                        }));
                        services.AddProfileService();
                    });
                });

            client = localWebApplicationFactory.CreateClient();

            return localWebApplicationFactory;
        }
    }
}