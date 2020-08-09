using TicketFlow.Common.Providers;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models.CinemaHallModels;
using TicketFlow.MovieService.Persistence.EntityModels;
using TicketFlow.MovieService.Service.Factories;

namespace TicketFlow.MovieService.Persistence
{
    internal class CinemaHallRepository : PostgresCrudRepositoryBase<int, ICinemaHall, StoredCinemaHallCreationModel, CinemaHallDatabaseModel>, ICinemaHallRepository
    {
        private const string SelectMapping = "id AS Id, name AS Name, location AS Location, seat_rows as SeatRows, seats_in_row as SeatsInRow";
        private const string TableName = "tickets";

        public CinemaHallRepository(IPostgresDbConnectionProvider dbConnectionProvider, IEntityFactory<ICinemaHall, StoredCinemaHallCreationModel> entityFactory)
            : base(dbConnectionProvider, entityFactory)
        {
        }

        protected override string SelectByIdentifierQuery => $"SELECT {SelectMapping} FROM {TableName} WHERE id = @Id;";

        protected override string SelectAllQuery => $"SELECT {SelectMapping} FROM {TableName};";

        protected override string InsertQuery => $"INSERT INTO {TableName} (id, name, location, seat_rows, seats_in_row) VALUES (@Id, @Name, @Location, @SeatRows, @SeatsInRow);";

        protected override string UpdateQuery => $"UPDATE {TableName} SET name=@Name, location=@Location, seat_rows=@SeatRows, seats_in_row=@SeatsInRow WHERE id = @Id;";

        protected override string DeleteQuery => $"DELETE FROM {TableName} WHERE id = @Id;";

        protected override StoredCinemaHallCreationModel Convert(CinemaHallDatabaseModel databaseModel)
            => new StoredCinemaHallCreationModel(databaseModel.Id, databaseModel.Name, databaseModel.Location, databaseModel.SeatRows, databaseModel.SeatsInRow);

        protected override CinemaHallDatabaseModel Convert(ICinemaHall entity)
            => new CinemaHallDatabaseModel { Id = entity.Id, Name = entity.Name, Location = entity.Location, SeatRows = entity.SeatRows, SeatsInRow = entity.SeatsInRow };
    }
}