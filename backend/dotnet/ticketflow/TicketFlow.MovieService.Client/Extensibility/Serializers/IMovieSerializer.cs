using TicketFlow.Common.Serializers;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;

namespace TicketFlow.MovieService.Client.Extensibility.Serializers
{
    public interface IMovieSerializer : IEntitySerializer<IMovie, MovieSerializationModel>
    {
    }
}