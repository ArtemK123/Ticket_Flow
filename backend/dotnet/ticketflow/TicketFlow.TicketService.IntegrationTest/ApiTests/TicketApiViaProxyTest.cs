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
using TicketFlow.TicketService.Client.Extensibility.Models;
using TicketFlow.TicketService.Client.Extensibility.Proxies;
using TicketFlow.TicketService.IntegrationTest.Fakes;
using TicketFlow.TicketService.Persistence;
using Xunit;

namespace TicketFlow.TicketService.IntegrationTest.ApiTests
{
    public class TicketApiViaProxyTest
    {
        [Fact]
        public async Task Add_ThenGetByMovieId_ShouldReturnTicketsWithGivenMovieId()
        {
            using WebApplicationFactory<Startup> webApplicationFactory = SetupWebApplicationFactory();
            ITicketApiProxy proxy = webApplicationFactory.Services.GetService<ITicketApiProxy>();

            const int movieId = 1;
            const int anotherMovieId = 100;

            IReadOnlyCollection<TicketCreationModel> ticketCreationModelsToAdd = new[]
            {
                new TicketCreationModel(movieId, 1, 1, 70),
                new TicketCreationModel(movieId, 2, 2, 90),
                new TicketCreationModel(movieId, 3, 5, 100),
                new TicketCreationModel(anotherMovieId, 3, 5, 150),
            };
            await Task.WhenAll(ticketCreationModelsToAdd.Select(model => proxy.AddAsync(model)));

            IReadOnlyCollection<ITicket> tickets = await proxy.GetByMovieIdAsync(movieId);

            IReadOnlyCollection<TicketCreationModel> ticketCreationApiModelsWithGivenMovieId = ticketCreationModelsToAdd.Where(model => model.MovieId == movieId).ToArray();
            Assert.True(ticketCreationApiModelsWithGivenMovieId.All(ticketCreationModel => tickets.Any(ticket => SameTicket(ticketCreationModel, ticket))));
        }

        [Fact]
        public async Task Order_ShouldUpdateTicketWithUserEmail() {
            using WebApplicationFactory<Startup> webApplicationFactory = SetupWebApplicationFactory();
            ITicketApiProxy proxy = webApplicationFactory.Services.GetService<ITicketApiProxy>();

            TicketCreationModel ticketCreationModel = new TicketCreationModel(1, 1, 1, 70);
            await proxy.AddAsync(ticketCreationModel);
            ITicket ticket = (await proxy.GetByMovieIdAsync(ticketCreationModel.MovieId)).First();

            const string userEmail = "test@gmail.com";
            await proxy.OrderAsync(new OrderModel(ticket.Id, userEmail));

            // IOrderedTicket orderedTicket = (await proxy.GetByUserEmailAsync(userEmail)).First();
            //
            // Assert.True(orderedTicket.)
        }

        private static bool SameTicket(TicketCreationModel creationModel, ITicket ticket)
            => creationModel.MovieId == ticket.MovieId
               && creationModel.Row == ticket.Row
               && creationModel.Seat == ticket.Seat
               && creationModel.Price == ticket.Price;

        private WebApplicationFactory<Startup> SetupWebApplicationFactory()
        {
            HttpClient client = default;

            WebApplicationFactory<Startup> webApplicationFactory = new WebApplicationFactory<Startup>()
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

            client = webApplicationFactory.CreateClient();

            return webApplicationFactory;
        }
    }
}