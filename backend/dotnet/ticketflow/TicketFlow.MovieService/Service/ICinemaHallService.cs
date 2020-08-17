using System.Collections.Generic;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;

namespace TicketFlow.MovieService.Service
{
    public interface ICinemaHallService
    {
        IReadOnlyCollection<ICinemaHall> GetAll();

        ICinemaHall GetById(int id);

        ICinemaHall Add(CinemaHallCreationModel cinemaHallCreationModel);
    }
}