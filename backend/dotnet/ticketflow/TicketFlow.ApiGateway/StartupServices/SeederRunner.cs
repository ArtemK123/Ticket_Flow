using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketFlow.ApiGateway.StartupServices.Seeders;

namespace TicketFlow.ApiGateway.StartupServices
{
    internal class SeederRunner : ISeederRunner
    {
        private readonly ICinemaHallsSeeder cinemaHallsSeeder;
        private readonly IFilmsSeeder filmsSeeder;
        private readonly IMoviesSeeder moviesSeeder;
        private readonly ILogger logger;

        public SeederRunner(ICinemaHallsSeeder cinemaHallsSeeder, IFilmsSeeder filmsSeeder, IMoviesSeeder moviesSeeder, ILoggerFactory loggerFactory)
        {
            this.cinemaHallsSeeder = cinemaHallsSeeder;
            this.filmsSeeder = filmsSeeder;
            this.moviesSeeder = moviesSeeder;
            logger = loggerFactory.CreateLogger(nameof(SeederRunner));
        }

        public async Task RunSeedersAsync()
        {
            logger.LogInformation("Run seeders");

            await Task.WhenAll(
                    cinemaHallsSeeder.SeedAsync(),
                    filmsSeeder.SeedAsync())
                .ContinueWith(_ => moviesSeeder.SeedAsync());
        }
    }
}