using System.Collections.Generic;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models;
using TicketFlow.MovieService.Domain.Models.CinemaHallModels;

namespace TicketFlow.MovieService.Service
{
    public interface ICinemaHallService
    {
        IReadOnlyCollection<ICinemaHall> GetAll();

        ICinemaHall GetById(int id);

        ICinemaHall Add(CinemaHallCreationModel cinemaHallCreationModel);
    }
}