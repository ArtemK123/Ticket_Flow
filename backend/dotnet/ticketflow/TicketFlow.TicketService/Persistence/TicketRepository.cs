﻿using System.Collections.Generic;
using System.Linq;
using Dapper;
using TicketFlow.Common.Providers;
using TicketFlow.Common.Repositories;
using TicketFlow.TicketService.Domain.Entities;
using TicketFlow.TicketService.Domain.Models;
using TicketFlow.TicketService.Service.Serializers;

namespace TicketFlow.TicketService.Persistence
{
    internal class TicketRepository : MappedCrudRepositoryBase<int, ITicket, TicketSerializationModel>, ITicketRepository
    {
        private readonly ITicketSerializer ticketSerializer;

        public TicketRepository(IPostgresDbConnectionProvider dbConnectionProvider, ITicketSerializer ticketSerializer)
            : base(dbConnectionProvider)
        {
            this.ticketSerializer = ticketSerializer;
        }

        protected override string TableName => "tickets";

        protected override KeyValuePair<string, string> PrimaryKeyMapping => new KeyValuePair<string, string>("id", "Id");

        protected override IReadOnlyDictionary<string, string> NonPrimaryColumnsMapping => new Dictionary<string, string>
        {
            { "movie_id", "MovieId" },
            { "row", "Row" },
            { "seat", "Seat" },
            { "price", "Price" },
            { "buyer_email", "BuyerEmail" }
        };

        public IReadOnlyCollection<ITicket> GetByMovieId(int movieId)
            => SelectByField($"SELECT {GetSelectSqlMapping()} FROM {TableName} WHERE movie_id=@movieId;", new { movieId });

        public IReadOnlyCollection<ITicket> GetByBuyerEmail(string buyerEmail)
            => SelectByField($"SELECT {GetSelectSqlMapping()} FROM {TableName} WHERE buyer_email=@buyerEmail;", new { buyerEmail });

        protected override ITicket Convert(TicketSerializationModel databaseModel) => ticketSerializer.Deserialize(databaseModel);

        protected override TicketSerializationModel Convert(ITicket entity) => ticketSerializer.Serialize(entity);

        private IReadOnlyCollection<ITicket> SelectByField(string sql, object sqlParams)
        {
            using var dbConnection = DbConnectionProvider.Get();
            IEnumerable<TicketSerializationModel> databaseModels = dbConnection.Query<TicketSerializationModel>(sql, sqlParams);
            return databaseModels.Select(Convert).ToList();
        }
    }
}