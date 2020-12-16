using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TicketFlow.TicketService.Client.Extensibility.DependencyInjection;
using TicketFlow.TicketService.Client.Extensibility.Entities;
using TicketFlow.TicketService.Client.Extensibility.Exceptions;
using TicketFlow.TicketService.Client.Extensibility.Models;
using TicketFlow.TicketService.Client.Extensibility.Proxies;
using TicketFlow.TicketService.IntegrationTest.Fakes;
using TicketFlow.TicketService.Persistence;
using Xunit;

namespace TicketFlow.TicketService.IntegrationTest.ApiTests
{
    public class TicketApiViaProxyTest : IDisposable
    {
        private const string UserEmail = "test@gmail.com";
        private const string AnotherUserEmail = "someboy@gmail.com";
        private const int MovieId = 1;
        private const int AnotherMovieId = 100;

        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly ITicketApiProxy proxy;

        public TicketApiViaProxyTest()
        {
            webApplicationFactory = SetupWebApplicationFactory();
            proxy = webApplicationFactory.Services.GetService<ITicketApiProxy>();
        }

        public void Dispose()
        {
            webApplicationFactory?.Dispose();
        }

        [Fact]
        public async Task Add_ThenGetByMovieId_ShouldReturnTicketsWithGivenMovieId()
        {
            IReadOnlyCollection<TicketCreationModel> ticketCreationModelsToAdd = new[]
            {
                new TicketCreationModel(MovieId, 1, 1, 70),
                new TicketCreationModel(MovieId, 2, 2, 90),
                new TicketCreationModel(MovieId, 3, 5, 100),
                new TicketCreationModel(AnotherMovieId, 3, 5, 150)
            };

            foreach (var creationModel in ticketCreationModelsToAdd)
            {
                await proxy.AddAsync(creationModel);
            }

            IReadOnlyCollection<ITicket> tickets = await proxy.GetByMovieIdAsync(MovieId);

            IReadOnlyCollection<TicketCreationModel> ticketCreationApiModelsWithGivenMovieId = ticketCreationModelsToAdd.Where(model => model.MovieId == MovieId).ToArray();
            Assert.True(ticketCreationApiModelsWithGivenMovieId.All(ticketCreationModel => tickets.Any(ticket => SameTicket(ticketCreationModel, ticket))));
        }

        [Fact]
        public async Task Order_Valid_ShouldUpdateTicketWithUserEmail()
        {
            TicketCreationModel ticketCreationModel = new TicketCreationModel(1, 1, 1, 70);
            await proxy.AddAsync(ticketCreationModel);
            ITicket ticket = (await proxy.GetByMovieIdAsync(ticketCreationModel.MovieId)).First();

            await proxy.OrderAsync(new OrderModel(ticket.Id, UserEmail));

            IOrderedTicket orderedTicket = (await proxy.GetByUserEmailAsync(UserEmail)).First();

            Assert.True(orderedTicket.BuyerEmail == UserEmail && SameTicket(ticketCreationModel, orderedTicket));
        }

        [Fact]
        public async Task Order_AlreadyOrderedTicket_ShouldThrowTicketAlreadyOrderedException()
        {
            TicketCreationModel ticketCreationModel = new TicketCreationModel(1, 1, 1, 70);
            await proxy.AddAsync(ticketCreationModel);
            ITicket ticket = (await proxy.GetByMovieIdAsync(ticketCreationModel.MovieId)).First();

            await proxy.OrderAsync(new OrderModel(ticket.Id, UserEmail));

            TicketAlreadyOrderedException exception = await Assert.ThrowsAsync<TicketAlreadyOrderedException>(
                async () => await proxy.OrderAsync(new OrderModel(ticket.Id, UserEmail)));

            Assert.Equal($"Ticket with id={ticket.Id} is already ordered", exception.Message);
        }

        [Fact]
        public async Task Order_WrongTicketId_ShouldThrowTicketNotFoundByIdException()
        {
            const int notFoundTicketId = 1;
            TicketNotFoundByIdException exception = await Assert.ThrowsAsync<TicketNotFoundByIdException>(
                async () => await proxy.OrderAsync(new OrderModel(notFoundTicketId, UserEmail)));

            Assert.Equal($"Ticket with id={notFoundTicketId} is not found", exception.Message);
        }

        [Fact]
        public async Task Add_ThenOrderSome_ThenGetByUserEmail_ShouldReturnAllTicketsWithGivenUserEmail()
        {
            IReadOnlyCollection<TicketCreationModel> notOrderedTickets = new[]
            {
                CreateDefaultTicketCreationModel(),
                CreateDefaultTicketCreationModel()
            };

            IReadOnlyCollection<(TicketCreationModel, string)> ticketToOrderWithUserEmailPairs = new[]
            {
                (CreateDefaultTicketCreationModel(), UserEmail),
                (CreateDefaultTicketCreationModel(), UserEmail),
                (CreateDefaultTicketCreationModel(), AnotherUserEmail)
            };

            foreach (TicketCreationModel ticketCreationModel in notOrderedTickets)
            {
                await proxy.AddAsync(ticketCreationModel);
            }

            foreach ((TicketCreationModel ticketCreationModel, string userEmail) in ticketToOrderWithUserEmailPairs)
            {
                var ticketId = await proxy.AddAsync(ticketCreationModel);
                await proxy.OrderAsync(new OrderModel(ticketId, userEmail));
            }

            IReadOnlyCollection<TicketCreationModel> expectedTicketModels = ticketToOrderWithUserEmailPairs
                .Where(ticketWithEmail => ticketWithEmail.Item2 == UserEmail)
                .Select(ticketWithEmail => ticketWithEmail.Item1)
                .ToArray();

            IReadOnlyCollection<IOrderedTicket> actual = await proxy.GetByUserEmailAsync(UserEmail);

            Assert.True(expectedTicketModels.Count == actual.Count && expectedTicketModels.All(ticketCreationModel => actual.Any(ticket => SameTicket(ticketCreationModel, ticket))));
        }

        private static bool SameTicket(TicketCreationModel creationModel, ITicket ticket)
            => creationModel.MovieId == ticket.MovieId
               && creationModel.Row == ticket.Row
               && creationModel.Seat == ticket.Seat
               && creationModel.Price == ticket.Price;

        private static TicketCreationModel CreateDefaultTicketCreationModel() => new TicketCreationModel(default, default, default, default);

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
                        services.AddSingleton<ITicketRepository, FakeTicketRepository>();
                        services.AddSingleton(serviceProvider => new Lazy<IHttpClientFactory>(() =>
                        {
                            var mock = Substitute.For<IHttpClientFactory>();

                            // ReSharper disable once AccessToModifiedClosure - It is expected closure, which is used for testing service proxy
                            mock.CreateClient(default).ReturnsForAnyArgs(client);
                            return mock;
                        }));
                        services.AddTicketService();
                    });
                });

            client = localWebApplicationFactory.CreateClient();

            return localWebApplicationFactory;
        }
    }
}