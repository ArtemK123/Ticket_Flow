using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TicketFlow.ApiGateway.StartupServices.Seeders;

namespace TicketFlow.ApiGateway.StartupServices
{
    internal class ApiGatewayStartupSeeder : IApiGatewayStartupSeeder
    {
        private readonly ICinemaHallsSeeder cinemaHallsSeeder;
        private readonly IFilmsSeeder filmsSeeder;
        private readonly IMoviesSeeder moviesSeeder;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public ApiGatewayStartupSeeder(
            ICinemaHallsSeeder cinemaHallsSeeder,
            IFilmsSeeder filmsSeeder,
            IMoviesSeeder moviesSeeder,
            ILoggerFactory loggerFactory,
            IConfiguration configuration)
        {
            this.cinemaHallsSeeder = cinemaHallsSeeder;
            this.filmsSeeder = filmsSeeder;
            this.moviesSeeder = moviesSeeder;
            this.configuration = configuration;
            logger = loggerFactory.CreateLogger(nameof(ApiGatewayStartupSeeder));
        }

        public async Task SeedAsync()
        {
            bool runSeeders = configuration.GetValue<bool>("TicketFlow:RunSeeders");

            if (runSeeders)
            {
                logger.LogInformation("Run seeders");

                await Task.WhenAll(
                        cinemaHallsSeeder.SeedAsync(),
                        filmsSeeder.SeedAsync())
                    .ContinueWith(task =>
                    {
                        if (task.IsFaulted && task.Exception != null)
                        {
                            throw task.Exception.InnerExceptions.First();
                        }

                        return moviesSeeder.SeedAsync();
                    });
            }
        }
    }
}