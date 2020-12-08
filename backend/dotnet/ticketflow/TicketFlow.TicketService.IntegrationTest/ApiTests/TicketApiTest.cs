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
using TicketFlow.TicketService.Client.Extensibility.Models;
using TicketFlow.TicketService.IntegrationTest.Fakes;
using TicketFlow.TicketService.Persistence;
using TicketFlow.TicketService.WebApi.ClientModels;
using Xunit;

namespace TicketFlow.TicketService.IntegrationTest.ApiTests
{
    public class TicketApiTest : IClassFixture<WebApplicationFactory<Startup>>
    {
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

        [Fact]
        public async Task Add_ThenGetByMovieId_ShouldReturnTicketsWithGivenMovieId()
        {
            const int movieId = 1;
            const int anotherMovieId = 100;
            HttpClient client = CreateServiceClient();

            IReadOnlyCollection<TicketCreationApiModel> ticketCreationApiModelsToAdd = new[]
            {
                new TicketCreationApiModel { MovieId = movieId, Row = 1, Seat = 1, Price = 70 },
                new TicketCreationApiModel { MovieId = movieId, Row = 2, Seat = 2, Price = 90 },
                new TicketCreationApiModel { MovieId = movieId, Row = 3, Seat = 5, Price = 100 },
                new TicketCreationApiModel { MovieId = anotherMovieId, Row = 3, Seat = 5, Price = 150 }
            };
            await Task.WhenAll(ticketCreationApiModelsToAdd.Select(model => AddTicketAsync(client, model)));

            IReadOnlyCollection<TicketSerializationModel> tickets = await GetTicketsByMovieAsync(client, movieId);

            IReadOnlyCollection<TicketCreationApiModel> ticketCreationApiModelsWithGivenMovieId = ticketCreationApiModelsToAdd.Where(model => model.MovieId == movieId).ToArray();
            Assert.True(ticketCreationApiModelsWithGivenMovieId.All(ticketCreationModel => tickets.Any(ticket => SameTicket(ticketCreationModel, ticket))));
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