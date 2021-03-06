﻿using TicketFlow.TicketService.Client.Extensibility.Entities;

namespace TicketFlow.TicketService.Client.Entities
{
    internal class OrderedTicket : Ticket, IOrderedTicket
    {
        public OrderedTicket(int id, int movieId, int row, int seat, int price, string buyerEmail)
            : base(id, movieId, row, seat, price)
        {
            BuyerEmail = buyerEmail;
        }

        public string BuyerEmail { get; }
    }
}