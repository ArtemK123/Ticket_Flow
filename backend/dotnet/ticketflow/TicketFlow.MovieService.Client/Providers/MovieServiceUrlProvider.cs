using System.Threading.Tasks;
using TicketFlow.Common.ServiceUrl.Providers;

namespace TicketFlow.MovieService.Client.Providers
{
    internal class MovieServiceUrlProvider : IMovieServiceUrlProvider
    {
        private readonly IServiceUrlProvider serviceUrlProvider;

        public MovieServiceUrlProvider(IServiceUrlProvider serviceUrlProvider)
        {
            this.serviceUrlProvider = serviceUrlProvider;
        }

        public Task<string> GetUrlAsync() => serviceUrlProvider.GetUrlAsync("MovieService");
    }
}