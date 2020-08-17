using TicketFlow.Common.Factories;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;

namespace TicketFlow.MovieService.Client.Extensibility.Factories
{
    public interface ICinemaHallFactory : IEntityFactory<ICinemaHall, CinemaHallCreationModel>, IEntityFactory<ICinemaHall, StoredCinemaHallCreationModel>
    {
    }
}