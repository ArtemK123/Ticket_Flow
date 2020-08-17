using TicketFlow.Common.Providers;
using TicketFlow.MovieService.Client.Entities;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Factories;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;

namespace TicketFlow.MovieService.Client.Factories
{
    internal class CinemaHallFactory : ICinemaHallFactory
    {
        private readonly IRandomValueProvider randomValueProvider;

        public CinemaHallFactory(IRandomValueProvider randomValueProvider)
        {
            this.randomValueProvider = randomValueProvider;
        }

        public ICinemaHall Create(StoredCinemaHallCreationModel creationModel)
            => new CinemaHall(creationModel.Id, creationModel.Name, creationModel.Location, creationModel.SeatRows, creationModel.SeatsInRow);

        public ICinemaHall Create(CinemaHallCreationModel creationModel)
        {
            int id = randomValueProvider.GetRandomInt(0, int.MaxValue);
            return new CinemaHall(id, creationModel.Name, creationModel.Location, creationModel.SeatRows, creationModel.SeatsInRow);
        }
    }
}