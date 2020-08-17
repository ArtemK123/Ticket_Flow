using System.Collections.Generic;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;
using TicketFlow.MovieService.Client.Extensibility.Proxies;

namespace TicketFlow.MovieService.Client.Proxies
{
    internal class CinemaHallApiProxy : ICinemaHallApiProxy
    {
        public IReadOnlyCollection<ICinemaHall> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public int Add(CinemaHallCreationModel cinemaHallCreationModel)
        {
            throw new System.NotImplementedException();
        }
    }
}