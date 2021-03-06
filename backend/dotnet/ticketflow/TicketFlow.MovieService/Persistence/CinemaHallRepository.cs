﻿using System.Collections.Generic;
using TicketFlow.Common.Providers;
using TicketFlow.Common.Repositories;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Factories;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;
using TicketFlow.MovieService.Persistence.EntityModels;

namespace TicketFlow.MovieService.Persistence
{
    internal class CinemaHallRepository : FactoryCrudRepositoryBase<int, ICinemaHall, CinemaHallDatabaseModel, StoredCinemaHallCreationModel>, ICinemaHallRepository
    {
        public CinemaHallRepository(IPostgresDbConnectionProvider dbConnectionProvider, ICinemaHallFactory entityFactory)
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

        protected override StoredCinemaHallCreationModel ConvertToFactoryModel(CinemaHallDatabaseModel databaseModel)
            => new StoredCinemaHallCreationModel(databaseModel.Id, databaseModel.Name, databaseModel.Location, databaseModel.SeatRows, databaseModel.SeatsInRow);

        protected override CinemaHallDatabaseModel Convert(ICinemaHall entity)
            => new CinemaHallDatabaseModel { Id = entity.Id, Name = entity.Name, Location = entity.Location, SeatRows = entity.SeatRows, SeatsInRow = entity.SeatsInRow };
    }
}