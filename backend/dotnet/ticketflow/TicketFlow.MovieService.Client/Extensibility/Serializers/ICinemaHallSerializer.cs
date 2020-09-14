using TicketFlow.Common.Serializers;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;

namespace TicketFlow.MovieService.Client.Extensibility.Serializers
{
    public interface ICinemaHallSerializer : IEntitySerializer<ICinemaHall, CinemaHallSerializationModel>
    {
    }
}