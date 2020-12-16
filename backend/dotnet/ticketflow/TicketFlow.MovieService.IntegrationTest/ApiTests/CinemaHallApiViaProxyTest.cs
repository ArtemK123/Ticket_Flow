using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;
using TicketFlow.MovieService.Client.Extensibility.Proxies;
using Xunit;

namespace TicketFlow.MovieService.IntegrationTest.ApiTests
{
    public class CinemaHallApiViaProxyTest : IDisposable
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly ICinemaHallApiProxy proxy;

        public CinemaHallApiViaProxyTest()
        {
            webApplicationFactory = new WebApplicationFactoryProvider().GetWebApplicationFactory();
            proxy = webApplicationFactory.Services.GetService<ICinemaHallApiProxy>();
        }

        public void Dispose()
        {
            webApplicationFactory?.Dispose();
        }

        [Fact]
        public async Task Add_ThenGetAll_ShouldReturnAllCinemaHalls()
        {
            IReadOnlyCollection<CinemaHallCreationModel> creationModels = new[]
            {
                new CinemaHallCreationModel("Super Cinema", "Kyiv", 5, 6),
                new CinemaHallCreationModel("test", "test", 2, 3),
                new CinemaHallCreationModel("Name", "location", 2, 2)
            };

            List<int> addedCinemaHallIds = new List<int>();

            foreach (CinemaHallCreationModel creationModel in creationModels)
            {
                int id = await proxy.AddAsync(creationModel);
                addedCinemaHallIds.Add(id);
            }

            IReadOnlyCollection<ICinemaHall> fetchedCinemaHalls = await proxy.GetAllAsync();

            Assert.True(fetchedCinemaHalls.All(cinemaHall => creationModels.Any(creationModel => SameCinemaHall(cinemaHall, creationModel))));
            Assert.True(fetchedCinemaHalls.All(cinemaHall => addedCinemaHallIds.Contains(cinemaHall.Id)));
        }

        private static bool SameCinemaHall(ICinemaHall cinemaHall, CinemaHallCreationModel creationModel)
            => cinemaHall.Name == creationModel.Name
               && cinemaHall.Location == creationModel.Location
               && cinemaHall.SeatRows == creationModel.SeatRows
               && cinemaHall.SeatsInRow == creationModel.SeatsInRow;
    }
}