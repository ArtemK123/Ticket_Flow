using System.Collections.Generic;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;

namespace TicketFlow.MovieService.Client.Extensibility.Proxies
{
    public interface ICinemaHallApiProxy
    {
        public IReadOnlyCollection<ICinemaHall> GetAll();

        public int Add(CinemaHallCreationModel cinemaHallCreationModel);
    }
}