using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Factories;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;
using TicketFlow.MovieService.Client.Extensibility.Serializers;

namespace TicketFlow.MovieService.Client.Serializers
{
    internal class MovieSerializer : IMovieSerializer
    {
        private readonly IMovieFactory movieFactory;
        private readonly IFilmSerializer filmSerializer;
        private readonly ICinemaHallSerializer cinemaHallSerializer;

        public MovieSerializer(IMovieFactory movieFactory, IFilmSerializer filmSerializer, ICinemaHallSerializer cinemaHallSerializer)
        {
            this.movieFactory = movieFactory;
            this.filmSerializer = filmSerializer;
            this.cinemaHallSerializer = cinemaHallSerializer;
        }

        public MovieSerializationModel Serialize(IMovie entity)
        {
            FilmSerializationModel film = filmSerializer.Serialize(entity.Film);
            CinemaHallSerializationModel cinemaHall = cinemaHallSerializer.Serialize(entity.CinemaHall);

            return new MovieSerializationModel { Id = entity.Id, StartTime = entity.StartTime, Film = film, CinemaHall = cinemaHall };
        }

        public IMovie Deserialize(MovieSerializationModel serializationModel)
        {
            IFilm film = filmSerializer.Deserialize(serializationModel.Film);
            ICinemaHall cinemaHall = cinemaHallSerializer.Deserialize(serializationModel.CinemaHall);

            return movieFactory.Create(new StoredMovieCreationModel(serializationModel.Id, serializationModel.StartTime, film, cinemaHall));
        }
    }
}