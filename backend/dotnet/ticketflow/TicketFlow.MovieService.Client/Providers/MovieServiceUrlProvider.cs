using Microsoft.Extensions.Configuration;

namespace TicketFlow.MovieService.Client.Providers
{
    internal class MovieServiceUrlProvider : IMovieServiceUrlProvider
    {
        private readonly IConfiguration configuration;

        public MovieServiceUrlProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetUrl() => configuration.GetValue<string>("TicketFlow:MovieService:Url");
    }
}