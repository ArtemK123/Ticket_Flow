using System.Collections.Generic;
using System.Linq;
using Dapper;
using TicketFlow.Common.Providers;
using TicketFlow.TicketService.Domain.Entities;
using TicketFlow.TicketService.Persistence.EntityModels;

namespace TicketFlow.TicketService.Persistence
{
    internal class TicketRepository : ITicketRepository
    {
        private const string SelectMapping = "id AS Id, movie_id AS MovieId, buyer_email AS BuyerEmail, row as Row, seat as Seat, price as Price";
        private const string TableName = "tickets";

        private readonly IPostgresDbConnectionProvider dbConnectionProvider;

        public TicketRepository(IPostgresDbConnectionProvider dbConnectionProvider)
        {
            this.dbConnectionProvider = dbConnectionProvider;
        }

        public bool TryGetById(int id, out ITicket ticket)
        {
            var sql = $"SELECT {SelectMapping} FROM {TableName} WHERE id = @id;";
            using var dbConnection = dbConnectionProvider.Get();
            TicketDatabaseModel databaseModel = dbConnection.Query<TicketDatabaseModel>(sql, new { id }).SingleOrDefault();
            ticket = Convert(databaseModel);
            return databaseModel != null;
        }

        public IReadOnlyCollection<ITicket> GetByMovieId(int movieId)
        {
            var sql = $"SELECT {SelectMapping} FROM {TableName} WHERE movie_id = @movieId;";
            using var dbConnection = dbConnectionProvider.Get();
            IEnumerable<TicketDatabaseModel> databaseModels = dbConnection.Query<TicketDatabaseModel>(sql, new { movieId });

            return databaseModels.Select(Convert).ToList();
        }

        public IReadOnlyCollection<ITicket> GetByBuyerEmail(string buyerEmail)
        {
            var sql = $"SELECT {SelectMapping} FROM {TableName} WHERE buyer_email = @buyerEmail;";
            using var dbConnection = dbConnectionProvider.Get();
            IEnumerable<TicketDatabaseModel> databaseModels = dbConnection.Query<TicketDatabaseModel>(sql, new { buyerEmail });

            return databaseModels.Select(Convert).ToList();
        }

        public void Add(ITicket ticket)
        {
            using var dbConnection = dbConnectionProvider.Get();
            var sql = $"INSERT INTO {TableName} (id, buyer_email, movie_id, row, seat, price) VALUES (@Id, @BuyerEmail, @MovieId, @Row, @Seat, @Price);";
            dbConnection.Execute(sql, Convert(ticket));
        }

        public void Update(ITicket ticket)
        {
            var sql = $"UPDATE {TableName} SET buyer_email=@BuyerEmail, movie_id=@MovieId, row=@Row, seat=@Seat, price=@Price WHERE id = @Id";

            using var dbConnection = dbConnectionProvider.Get();
            dbConnection.Execute(sql, Convert(ticket));
        }

        private static ITicket Convert(TicketDatabaseModel databaseModel)
            => databaseModel != null ?
                new Ticket(databaseModel.Id, databaseModel.MovieId, databaseModel.BuyerEmail, databaseModel.Row, databaseModel.Seat, databaseModel.Price)
                : null;

        private static TicketDatabaseModel Convert(ITicket ticket)
            => ticket != null ?
                new TicketDatabaseModel { Id = ticket.Id, MovieId = ticket.MovieId, BuyerEmail = ticket.BuyerEmail, Row = ticket.Row, Seat = ticket.Seat, Price = ticket.Price }
                : null;
    }
}