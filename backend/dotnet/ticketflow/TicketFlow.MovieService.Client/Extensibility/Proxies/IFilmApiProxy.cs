using System.Collections.Generic;
using System.Threading.Tasks;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;

namespace TicketFlow.MovieService.Client.Extensibility.Proxies
{
    public interface IFilmApiProxy
    {
        public Task<IReadOnlyCollection<IFilm>> GetAllAsync();

        public Task<int> AddAsync(FilmCreationModel filmCreationModel);
    }
}