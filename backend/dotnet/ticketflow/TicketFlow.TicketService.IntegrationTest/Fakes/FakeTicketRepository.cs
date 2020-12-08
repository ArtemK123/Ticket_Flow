using System.Collections.Generic;
using System.Linq;
using TicketFlow.TicketService.Client.Extensibility.Entities;
using TicketFlow.TicketService.Client.Extensibility.Models;
using TicketFlow.TicketService.Client.Extensibility.Serializers;
using TicketFlow.TicketService.Persistence;

namespace TicketFlow.TicketService.IntegrationTest.Fakes
{
    internal class FakeTicketRepository : ITicketRepository
    {
        private readonly ITicketSerializer ticketSerializer;
        private readonly List<TicketSerializationModel> storedTicketModels;

        public FakeTicketRepository(ITicketSerializer ticketSerializer)
        {
            this.ticketSerializer = ticketSerializer;
            storedTicketModels = new List<TicketSerializationModel>();
        }

        public bool TryGet(int identifier, out ITicket entity)
        {
            TicketSerializationModel ticketModel = storedTicketModels.FirstOrDefault(model => model.Id == identifier);
            if (ticketModel != null)
            {
                entity = ticketSerializer.Deserialize(ticketModel);
                return true;
            }

            entity = default;
            return false;
        }

        public IReadOnlyCollection<ITicket> GetAll()
            => storedTicketModels.Select(ticket => ticketSerializer.Deserialize(ticket)).ToArray();

        public void Add(ITicket entity)
        {
            TicketSerializationModel model = ticketSerializer.Serialize(entity);
            storedTicketModels.Add(model);
        }

        public void Update(ITicket entity)
        {
            if (TryGet(entity.Id, out ITicket _))
            {
                Delete(entity.Id);
                Add(entity);
            }
        }

        public void Delete(int identifier)
        {
            TicketSerializationModel ticketModel = storedTicketModels.FirstOrDefault(model => model.Id == identifier);
            if (ticketModel != null)
            {
                storedTicketModels.Remove(ticketModel);
            }
        }

        public IReadOnlyCollection<ITicket> GetByMovieId(int movieId)
            => storedTicketModels
                .Where(model => model.MovieId == movieId)
                .Select(model => ticketSerializer.Deserialize(model))
                .ToArray();

        public IReadOnlyCollection<ITicket> GetByBuyerEmail(string buyerEmail)
            => storedTicketModels
                .Where(model => model.BuyerEmail == buyerEmail)
                .Select(model => ticketSerializer.Deserialize(model))
                .ToArray();
    }
}