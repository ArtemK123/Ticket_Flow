using System.Collections.Generic;
using TicketFlow.Common.Providers;
using TicketFlow.Common.Repositories;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Factories;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;
using TicketFlow.MovieService.Persistence.EntityModels;
using TicketFlow.MovieService.Service;

namespace TicketFlow.MovieService.Persistence
{
    internal class MovieRepository : FactoryCrudRepositoryBase<int, IMovie, MovieDatabaseModel, StoredMovieCreationModel>, IMovieRepository
    {
        private readonly IFilmService filmService;
        private readonly ICinemaHallService cinemaHallService;

        public MovieRepository(
            IPostgresDbConnectionProvider dbConnectionProvider,
            IMovieFactory entityFactory,
            IFilmService filmService,
            ICinemaHallService cinemaHallService)
            : base(dbConnectionProvider, entityFactory)
        {
            this.filmService = filmService;
            this.cinemaHallService = cinemaHallService;
        }

        protected override string TableName => "movies";

        protected override KeyValuePair<string, string> PrimaryKeyMapping => new KeyValuePair<string, string>("id", "Id");

        protected override IReadOnlyDictionary<string, string> NonPrimaryColumnsMapping => new Dictionary<string, string>
        {
            { "start_time", "StartTime" },
            { "film_id", "FilmId" },
            { "cinema_hall_id", "CinemaHallId" }
        };

        protected override StoredMovieCreationModel ConvertToFactoryModel(MovieDatabaseModel databaseModel)
        {
            IFilm film = filmService.GetById(databaseModel.FilmId);
            ICinemaHall cinemaHall = cinemaHallService.GetById(databaseModel.CinemaHallId);

            return new StoredMovieCreationModel(databaseModel.Id, databaseModel.StartTime, film, cinemaHall);
        }

        protected override MovieDatabaseModel Convert(IMovie entity)
            => new MovieDatabaseModel { Id = entity.Id, StartTime = entity.StartTime, FilmId = entity.Film.Id, CinemaHallId = entity.CinemaHall.Id };
    }
}