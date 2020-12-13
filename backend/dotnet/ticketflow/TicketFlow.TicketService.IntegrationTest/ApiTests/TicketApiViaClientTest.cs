using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
using TicketFlow.TicketService.Service;
using TicketFlow.TicketService.WebApi.ClientModels;
using Xunit;

namespace TicketFlow.TicketService.IntegrationTest.ApiTests
{
    public class TicketApiViaClientTest
    {
        [Fact]
        public async Task ClientTest()
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
                        mock.CreateClient(default).ReturnsForAnyArgs(client);
                        return mock;
                    }));
                    services.AddTicketService();
                });
            });

            client = webApplicationFactory.CreateClient();
            const int movieId = 1;
            var creationApiModel = new TicketCreationApiModel { MovieId = movieId, Row = 1, Seat = 1, Price = 70 };

            ITicketApiProxy proxy = webApplicationFactory.Services.GetService<ITicketApiProxy>();

            await proxy.AddAsync(new TicketCreationModel(movieId, 1, 1, 70));

            Task<IReadOnlyCollection<ITicket>> tickets = proxy.GetByMovieIdAsync(movieId);
        }

        private static bool SameTicket(TicketCreationApiModel creationModel, TicketSerializationModel serializationModel)
            => creationModel.MovieId == serializationModel.MovieId
               && creationModel.Row == serializationModel.Row
               && creationModel.Seat == serializationModel.Seat
               && creationModel.Price == serializationModel.Price;

        private static Task AddTicketAsync(HttpClient httpClient, TicketCreationApiModel ticketCreationApiModel)
        {
            string serializedTicket = JsonSerializer.Serialize(ticketCreationApiModel);
            return httpClient.PostAsync("/tickets", new StringContent(serializedTicket, Encoding.UTF8, "application/json"));
        }

        private async Task<IReadOnlyCollection<TicketSerializationModel>> GetTicketsByMovieAsync(HttpClient client, int movieId)
        {
            HttpResponseMessage response = await client.GetAsync($"/tickets/by-movie/{movieId}");

            string responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TicketSerializationModel[]>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}