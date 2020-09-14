using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Factories;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;
using TicketFlow.MovieService.Client.Extensibility.Serializers;

namespace TicketFlow.MovieService.Client.Serializers
{
    internal class CinemaHallSerializer : ICinemaHallSerializer
    {
        private readonly ICinemaHallFactory cinemaHallFactory;

        public CinemaHallSerializer(ICinemaHallFactory cinemaHallFactory)
        {
            this.cinemaHallFactory = cinemaHallFactory;
        }

        public CinemaHallSerializationModel Serialize(ICinemaHall entity)
            => new CinemaHallSerializationModel
            {
                Id = entity.Id,
                Location = entity.Location,
                Name = entity.Name,
                SeatRows = entity.SeatRows,
                SeatsInRow = entity.SeatsInRow
            };

        public ICinemaHall Deserialize(CinemaHallSerializationModel serializationModel)
            => cinemaHallFactory.Create(
                new StoredCinemaHallCreationModel(serializationModel.Id, serializationModel.Name, serializationModel.Location, serializationModel.SeatRows, serializationModel.SeatsInRow));
    }
}