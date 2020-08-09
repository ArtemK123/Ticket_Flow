using TicketFlow.Common.Providers;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models.FilmModels;
using TicketFlow.MovieService.Persistence.EntityModels;
using TicketFlow.MovieService.Service.Factories;

namespace TicketFlow.MovieService.Persistence
{
    internal class FilmRepository : PostgresCrudRepositoryBase<int, IFilm, StoredFilmCreationModel, FilmDatabaseModel>, IFilmRepository
    {
        private const string SelectMapping
            = "id AS Id, title AS Title, description AS Description, premiere_date AS PremiereDate, creator AS Creator, duration AS Duration, age_limit AS AgeLimit";

        private const string TableName = "films";

        public FilmRepository(IPostgresDbConnectionProvider dbConnectionProvider, IEntityFactory<IFilm, StoredFilmCreationModel> entityFactory)
            : base(dbConnectionProvider, entityFactory)
        {
        }

        protected override string SelectByIdentifierQuery => $"SELECT {SelectMapping} FROM {TableName} WHERE id = @Id;";

        protected override string SelectAllQuery => $"SELECT {SelectMapping} FROM {TableName};";

        protected override string InsertQuery
            => $"INSERT INTO {TableName} (id, title, description, premiere_date, creator, duration, age_limit)" +
               "VALUES (@Id, @Title, @Description, @PremiereDate, @Creator, @Duration, @AgeLimit);";

        protected override string UpdateQuery
            => $"UPDATE {TableName} SET title=@Title, description=@Description, premiere_date=@PremiereDate, creator=@Creator, duration=@Duration, age_limit=@AgeLimit WHERE id = @Id;";

        protected override string DeleteQuery => $"DELETE FROM {TableName} WHERE id = @Id;";

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