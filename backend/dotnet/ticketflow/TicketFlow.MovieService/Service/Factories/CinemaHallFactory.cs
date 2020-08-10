using TicketFlow.Common.Factories;
using TicketFlow.Common.Providers;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models.CinemaHallModels;

namespace TicketFlow.MovieService.Service.Factories
{
    internal class CinemaHallFactory : EntityFactoryBase<ICinemaHall, CinemaHallCreationModel>, IEntityFactory<ICinemaHall, StoredCinemaHallCreationModel>
    {
        public CinemaHallFactory(IRandomValueProvider randomValueProvider)
            : base(randomValueProvider)
        {
        }

        public ICinemaHall Create(StoredCinemaHallCreationModel creationModel)
            => new CinemaHall(creationModel.Id, creationModel.Name, creationModel.Location, creationModel.SeatRows, creationModel.SeatsInRow);

        protected override ICinemaHall CreateEntityFromModel(int id, CinemaHallCreationModel creationModel)
            => new CinemaHall(id, creationModel.Name, creationModel.Location, creationModel.SeatRows, creationModel.SeatsInRow);
    }
}