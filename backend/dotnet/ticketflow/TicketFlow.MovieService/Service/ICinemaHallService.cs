using System.Collections.Generic;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models;

namespace TicketFlow.MovieService.Service
{
    public interface ICinemaHallService
    {
        IReadOnlyCollection<ICinemaHall> GetAll();

        ICinemaHall Add(CinemaHallCreationModel cinemaHallCreationModel);
    }
}