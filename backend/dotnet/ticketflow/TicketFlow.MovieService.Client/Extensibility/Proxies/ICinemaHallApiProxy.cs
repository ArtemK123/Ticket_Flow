using System.Collections.Generic;
using System.Threading.Tasks;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;

namespace TicketFlow.MovieService.Client.Extensibility.Proxies
{
    public interface ICinemaHallApiProxy
    {
        public Task<IReadOnlyCollection<ICinemaHall>> GetAllAsync();

        public Task<int> AddAsync(CinemaHallCreationModel cinemaHallCreationModel);
    }
}