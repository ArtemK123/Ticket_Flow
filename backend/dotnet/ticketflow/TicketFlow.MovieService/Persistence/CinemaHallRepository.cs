using System.Collections.Generic;
using TicketFlow.Common.Providers;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models.CinemaHallModels;
using TicketFlow.MovieService.Persistence.EntityModels;
using TicketFlow.MovieService.Service.Factories;

namespace TicketFlow.MovieService.Persistence
{
    internal class CinemaHallRepository : MappedPostgresCrudRepositoryBase<int, ICinemaHall, StoredCinemaHallCreationModel, CinemaHallDatabaseModel>, ICinemaHallRepository
    {
        public CinemaHallRepository(IPostgresDbConnectionProvider dbConnectionProvider, IEntityFactory<ICinemaHall, StoredCinemaHallCreationModel> entityFactory)
            : base(dbConnectionProvider, entityFactory)
        {
        }

        protected override string TableName => "cinema_halls";

        protected override KeyValuePair<string, string> PrimaryKeyMapping => new KeyValuePair<string, string>("id", "Id");

        protected override IReadOnlyDictionary<string, string> NonPrimaryColumnsMapping => new Dictionary<string, string>
        {
            { "name", "Name" },
            { "location", "Location" },
            { "seat_rows", "SeatRows" },
            { "seats_in_row", "SeatsInRow" },
        };

        protected override StoredCinemaHallCreationModel Convert(CinemaHallDatabaseModel databaseModel)
            => new StoredCinemaHallCreationModel(databaseModel.Id, databaseModel.Name, databaseModel.Location, databaseModel.SeatRows, databaseModel.SeatsInRow);

        protected override CinemaHallDatabaseModel Convert(ICinemaHall entity)
            => new CinemaHallDatabaseModel { Id = entity.Id, Name = entity.Name, Location = entity.Location, SeatRows = entity.SeatRows, SeatsInRow = entity.SeatsInRow };
    }
}