using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Factories;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;
using TicketFlow.MovieService.Client.Extensibility.Serializers;

namespace TicketFlow.MovieService.Client.Serializers
{
    internal class FilmSerializer : IFilmSerializer
    {
        private readonly IFilmFactory filmFactory;

        public FilmSerializer(IFilmFactory filmFactory)
        {
            this.filmFactory = filmFactory;
        }

        public FilmSerializationModel Serialize(IFilm entity)
            => new FilmSerializationModel
            {
                Id = entity.Id,
                AgeLimit = entity.AgeLimit,
                Creator = entity.Creator,
                Description = entity.Description,
                Duration = entity.Duration,
                PremiereDate = entity.PremiereDate,
                Title = entity.Title
            };

        public IFilm Deserialize(FilmSerializationModel serializationModel)
        {
            var creationModel = new StoredFilmCreationModel(
                serializationModel.Id,
                serializationModel.Title,
                serializationModel.Description,
                serializationModel.PremiereDate,
                serializationModel.Creator,
                serializationModel.Duration,
                serializationModel.AgeLimit);

            return filmFactory.Create(creationModel);
        }
    }
}