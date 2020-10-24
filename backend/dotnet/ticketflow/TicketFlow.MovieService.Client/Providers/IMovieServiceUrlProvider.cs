using System.Threading.Tasks;

namespace TicketFlow.MovieService.Client.Providers
{
    internal interface IMovieServiceUrlProvider
    {
        Task<string> GetUrlAsync();
    }
}