using TicketFlow.Common.Serializers;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;

namespace TicketFlow.MovieService.Client.Extensibility.Serializers
{
    public interface IFilmSerializer : IEntitySerializer<IFilm, FilmSerializationModel>
    {
    }
}