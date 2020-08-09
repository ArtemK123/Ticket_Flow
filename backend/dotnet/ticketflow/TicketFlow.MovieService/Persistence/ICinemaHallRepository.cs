using System.Collections.Generic;
using TicketFlow.MovieService.Domain.Entities;

namespace TicketFlow.MovieService.Persistence
{
    internal interface ICinemaHallRepository
    {
        bool TryGetById(int id, out ICinemaHall cinemaHall);

        IReadOnlyCollection<ICinemaHall> GetAll();

        void Add(ICinemaHall cinemaHall);
    }
}