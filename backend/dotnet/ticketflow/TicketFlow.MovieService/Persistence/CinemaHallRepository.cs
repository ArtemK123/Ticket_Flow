using System.Collections.Generic;
using System.Linq;
using Dapper;
using TicketFlow.Common.Providers;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models.CinemaHallModels;
using TicketFlow.MovieService.Persistence.EntityModels;
using TicketFlow.MovieService.Service.Factories;

namespace TicketFlow.MovieService.Persistence
{
    internal class CinemaHallRepository : ICinemaHallRepository
    {
        // private const string SelectMapping = "id AS Id, name AS Name, location AS Location, seat_rows as SeatRows, seats_in_row as SeatsInRow";
        // private const string TableName = "tickets";
        //
        // private readonly IPostgresDbConnectionProvider dbConnectionProvider;
        // private readonly ICinemaHallFactory cinemaHallFactory;
        //
        // public CinemaHallRepository(IPostgresDbConnectionProvider dbConnectionProvider, ICinemaHallFactory cinemaHallFactory)
        // {
        //     this.dbConnectionProvider = dbConnectionProvider;
        //     this.cinemaHallFactory = cinemaHallFactory;
        // }
        //
        // public bool TryGetById(int id, out ICinemaHall cinemaHall)
        // {
        //     var sql = $"SELECT {SelectMapping} FROM {TableName} WHERE id = @id;";
        //     using var dbConnection = dbConnectionProvider.Get();
        //     CinemaHallDatabaseModel databaseModel = dbConnection.Query<CinemaHallDatabaseModel>(sql, new { id }).SingleOrDefault();
        //     cinemaHall = Convert(databaseModel);
        //     return databaseModel != null;
        // }
        //
        // public IReadOnlyCollection<ICinemaHall> GetAll()
        // {
        //     var sql = $"SELECT {SelectMapping} FROM {TableName}";
        //     using var dbConnection = dbConnectionProvider.Get();
        //     IEnumerable<CinemaHallDatabaseModel> databaseModels = dbConnection.Query<CinemaHallDatabaseModel>(sql);
        //     return databaseModels.Select(Convert).ToList();
        // }
        //
        // public void Add(ICinemaHall cinemaHall)
        // {
        //     using var dbConnection = dbConnectionProvider.Get();
        //     var sql = $"INSERT INTO {TableName} (id, buyer_email, movie_id, row, seat, price) VALUES (@Id, @BuyerEmail, @MovieId, @Row, @Seat, @Price);";
        //     dbConnection.Execute(sql, Convert(cinemaHall));
        // }
        //
        // private static CinemaHallDatabaseModel Convert(ICinemaHall cinemaHall)
        //     => cinemaHall != null ?
        //         new CinemaHallDatabaseModel { Id = cinemaHall.Id, Name = cinemaHall.Name, Location = cinemaHall.Location, SeatRows = cinemaHall.SeatRows, SeatsInRow = cinemaHall.SeatsInRow }
        //         : null;
        //
        // private ICinemaHall Convert(CinemaHallDatabaseModel databaseModel)
        //     => databaseModel != null ?
        //         cinemaHallFactory.Create(new StoredCinemaHallCreationModel(databaseModel.Id, databaseModel.Name, databaseModel.Location, databaseModel.SeatRows, databaseModel.SeatsInRow))
        //         : null;
        public bool TryGetById(int id, out ICinemaHall cinemaHall)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<ICinemaHall> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Add(ICinemaHall cinemaHall)
        {
            throw new System.NotImplementedException();
        }
    }
}