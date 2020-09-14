using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;
using TicketFlow.MovieService.Client.Extensibility.Proxies;

namespace TicketFlow.ApiGateway.StartupServices.Seeders
{
    internal class CinemaHallsSeeder : ICinemaHallsSeeder
    {
        private readonly ICinemaHallApiProxy cinemaHallApiProxy;

        private readonly IReadOnlyCollection<CinemaHallCreationModel> cinemaHallsToSeed = new[]
        {
            new CinemaHallCreationModel("SuperLux", "Kyiv", 5, 5),
            new CinemaHallCreationModel("JustRelax", "Kyiv", 5, 5),
            new CinemaHallCreationModel("RedHall", "Lviv", 5, 5)
        };

        public CinemaHallsSeeder(ICinemaHallApiProxy cinemaHallApiProxy)
        {
            this.cinemaHallApiProxy = cinemaHallApiProxy;
        }

        public async Task SeedAsync()
        {
            IReadOnlyCollection<ICinemaHall> storedCinemaHalls = await cinemaHallApiProxy.GetAllAsync();
            if (storedCinemaHalls.Count == 0)
            {
                await Task.WhenAll(cinemaHallsToSeed.Select(cinemaHall => cinemaHallApiProxy.AddAsync(cinemaHall)));
            }
        }
    }
}