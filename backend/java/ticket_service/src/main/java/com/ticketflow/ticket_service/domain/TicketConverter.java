package com.ticketflow.ticket_service.domain;

import com.ticketflow.ticket_service.models.Ticket;

import org.springframework.stereotype.Component;

@Component
public class TicketConverter {
    public Ticket convert(TicketDatabaseModel ticketDatabaseModel) {
        return new Ticket(
            ticketDatabaseModel.getId(),
            ticketDatabaseModel.getMovieId(),
            ticketDatabaseModel.getBuyerEmail(),
            ticketDatabaseModel.getRow(),
            ticketDatabaseModel.getSeat(),
            ticketDatabaseModel.getPrice()
        );
    }

    public TicketDatabaseModel convert(Ticket ticket) {
        return new TicketDatabaseModel(
            ticket.getMovieId(),
            ticket.getBuyerEmail(),
            ticket.getRow(),
            ticket.getSeat(),
            ticket.getPrice()
        );
    }
}