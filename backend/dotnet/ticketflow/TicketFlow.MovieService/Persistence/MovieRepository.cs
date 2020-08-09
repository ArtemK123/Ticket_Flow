using TicketFlow.Common.Providers;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models.MovieModels;
using TicketFlow.MovieService.Persistence.EntityModels;
using TicketFlow.MovieService.Service;
using TicketFlow.MovieService.Service.Factories;

namespace TicketFlow.MovieService.Persistence
{
    internal class MovieRepository : PostgresCrudRepositoryBase<int, IMovie, StoredMovieCreationModel, MovieDatabaseModel>, IMovieRepository
    {
        private const string TableName = "movies";
        private const string SelectMapping = "id AS Id, start_time AS StartTime, film_id AS FilmId, cinema_hall_id AS CinemaHallId";

        private readonly IFilmService filmService;
        private readonly ICinemaHallService cinemaHallService;

        public MovieRepository(
            IPostgresDbConnectionProvider dbConnectionProvider,
            IEntityFactory<IMovie, StoredMovieCreationModel> entityFactory,
            IFilmService filmService,
            ICinemaHallService cinemaHallService)
            : base(dbConnectionProvider, entityFactory)
        {
            this.filmService = filmService;
            this.cinemaHallService = cinemaHallService;
        }

        protected override string SelectByIdentifierQuery => $"SELECT {SelectMapping} FROM {TableName} WHERE id = @Id;";

        protected override string SelectAllQuery => $"SELECT {SelectMapping} FROM {TableName};";

        protected override string InsertQuery => $"INSERT INTO {TableName} (id, start_time, film_id, cinema_hall_id) VALUES (@Id, @StartTime, @FilmId, @CinemaHallId);";

        protected override string UpdateQuery => $"UPDATE {TableName} SET start_time=@StartTime, film_id=@FilmId, cinema_hall_id=@CinemaHallId WHERE id = @Id;";

        protected override string DeleteQuery => $"DELETE FROM {TableName} WHERE id = @Id;";

        protected override StoredMovieCreationModel Convert(MovieDatabaseModel databaseModel)
        {
            IFilm film = filmService.GetById(databaseModel.FilmId);
            ICinemaHall cinemaHall = cinemaHallService.GetById(databaseModel.CinemaHallId);

            return new StoredMovieCreationModel(databaseModel.Id, databaseModel.StartTime, film, cinemaHall);
        }

        protected override MovieDatabaseModel Convert(IMovie entity)
            => new MovieDatabaseModel { Id = entity.Id, StartTime = entity.StartTime, FilmId = entity.Film.Id, CinemaHallId = entity.CinemaHall.Id };
    }
}