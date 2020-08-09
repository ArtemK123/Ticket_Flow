using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models.CinemaHallModels;

namespace TicketFlow.MovieService.Service.Factories
{
    public interface ICinemaHallFactory
    {
        ICinemaHall Create(CinemaHallCreationModel creationModel);

        ICinemaHall Create(StoredCinemaHallCreationModel creationModel);
    }
}