using System.Collections.Generic;
using TicketFlow.Common.Factories;
using TicketFlow.Common.Providers;
using TicketFlow.Common.Repositories;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models.FilmModels;
using TicketFlow.MovieService.Persistence.EntityModels;

namespace TicketFlow.MovieService.Persistence
{
    internal class FilmRepository : MappedPostgresCrudRepositoryBase<int, IFilm, StoredFilmCreationModel, FilmDatabaseModel>, IFilmRepository
    {
        public FilmRepository(IPostgresDbConnectionProvider dbConnectionProvider, IEntityFactory<IFilm, StoredFilmCreationModel> entityFactory)
            : base(dbConnectionProvider, entityFactory)
        {
        }

        protected override string TableName => "films";

        protected override KeyValuePair<string, string> PrimaryKeyMapping => new KeyValuePair<string, string>("id", "Id");

        protected override IReadOnlyDictionary<string, string> NonPrimaryColumnsMapping => new Dictionary<string, string>
        {
            { "title", "Title" },
            { "description", "Description" },
            { "premiere_date", "PremiereDate" },
            { "creator", "Creator" },
            { "duration", "Duration" },
            { "age_limit", "AgeLimit" }
        };

        protected override StoredFilmCreationModel Convert(FilmDatabaseModel databaseModel)
            => new StoredFilmCreationModel(
                databaseModel.Id,
                databaseModel.Title,
                databaseModel.Description,
                databaseModel.PremiereDate,
                databaseModel.Creator,
                databaseModel.Duration,
                databaseModel.AgeLimit);

        protected override FilmDatabaseModel Convert(IFilm entity)
            => new FilmDatabaseModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                PremiereDate = entity.PremiereDate,
                Creator = entity.Creator,
                Duration = entity.Duration,
                AgeLimit = entity.AgeLimit
            };
    }
}