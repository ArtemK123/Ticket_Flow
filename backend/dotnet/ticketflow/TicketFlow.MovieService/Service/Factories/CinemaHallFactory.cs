using TicketFlow.Common.Providers;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models.CinemaHallModels;

namespace TicketFlow.MovieService.Service.Factories
{
    internal class CinemaHallFactory : ICinemaHallFactory
    {
        private readonly IRandomValueProvider randomValueProvider;

        public CinemaHallFactory(IRandomValueProvider randomValueProvider)
        {
            this.randomValueProvider = randomValueProvider;
        }

        public ICinemaHall Create(CinemaHallCreationModel creationModel)
        {
            var id = randomValueProvider.GetRandomInt(0, int.MaxValue);
            return new CinemaHall(id, creationModel.Name, creationModel.Location, creationModel.SeatRows, creationModel.SeatsInRow);
        }

        public ICinemaHall Create(StoredCinemaHallCreationModel creationModel)
            => new CinemaHall(creationModel.Id, creationModel.Name, creationModel.Location, creationModel.SeatRows, creationModel.SeatsInRow);
    }
}