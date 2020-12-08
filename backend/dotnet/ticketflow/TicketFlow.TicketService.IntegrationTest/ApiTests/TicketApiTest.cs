using System.Collections.Generic;
using System.IO;
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
using TicketFlow.TicketService.Client.Extensibility.Models;
using TicketFlow.TicketService.IntegrationTest.Fakes;
using TicketFlow.TicketService.Persistence;
using TicketFlow.TicketService.WebApi.ClientModels;
using Xunit;

namespace TicketFlow.TicketService.IntegrationTest.ApiTests
{
    public class TicketApiTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private static readonly IReadOnlyCollection<TicketCreationApiModel> InitialTicketCreationApiModels = new[]
        {
            new TicketCreationApiModel { MovieId = 1, Row = 1, Seat = 1, Price = 100 },
            new TicketCreationApiModel { MovieId = 1, Row = 1, Seat = 2, Price = 100 },
            new TicketCreationApiModel { MovieId = 1, Row = 1, Seat = 3, Price = 100 },
            new TicketCreationApiModel { MovieId = 2, Row = 1, Seat = 3, Price = 50 }
        };

        private readonly WebApplicationFactory<Startup> webApplicationFactory;

        public TicketApiTest(WebApplicationFactory<Startup> webApplicationFactory)
        {
            this.webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task Index_ShouldReturnOk()
        {
            HttpClient client = CreateServiceClient();
            HttpResponseMessage response = await client.GetAsync("/");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(1)] // with tickets
        [InlineData(101)] // without tickets
        public async Task GetByMovieId_ShouldReturnTicketsWithGivenMovieId(int movieId)
        {
            HttpClient client = CreateServiceClient();

            List<Task> initializeTasks = new List<Task>();
            foreach (TicketCreationApiModel ticket in InitialTicketCreationApiModels)
            {
                string serializedTicket = JsonSerializer.Serialize(ticket);
                initializeTasks.Add(client.PostAsync("/tickets", new StringContent(serializedTicket, Encoding.UTF8, "application/json")));
            }

            await Task.WhenAll(initializeTasks);

            HttpResponseMessage response = await client.GetAsync($"/tickets/by-movie/{movieId}");

            string responseJson = await response.Content.ReadAsStringAsync();
            IReadOnlyCollection<TicketSerializationModel> tickets =
                JsonSerializer.Deserialize<TicketSerializationModel[]>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.True(tickets.All(ticket => ticket.MovieId == movieId && InitialTicketCreationApiModels.Any(creationModel => SameTicket(creationModel, ticket))));
            Assert.Equal(InitialTicketCreationApiModels.Count(ticket => ticket.MovieId == movieId), tickets.Count);
        }

        private static bool SameTicket(TicketCreationApiModel creationModel, TicketSerializationModel serializationModel)
            => creationModel.MovieId == serializationModel.MovieId
               && creationModel.Row == serializationModel.Row
               && creationModel.Seat == serializationModel.Seat
               && creationModel.Price == serializationModel.Price;


        // [Theory]
        // [InlineData("with.tickets@test.com")]
        // [InlineData("no.tickets@test.com")]
        // public async Task GetByUserEmail_ShouldReturnTicketsWithGivenUserEmail(string userEmail)
        // {
        // }

        private HttpClient CreateServiceClient()
            => webApplicationFactory
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Test");
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddSingleton(serviceProvider => Substitute.For<IMigrationRunner>());
                        services.AddSingleton<ITicketRepository, FakeTicketRepository>();
                    });
                })
                .CreateClient();
    }
}