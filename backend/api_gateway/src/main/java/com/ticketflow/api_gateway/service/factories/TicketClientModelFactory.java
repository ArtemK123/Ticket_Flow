package com.ticketflow.api_gateway.service.factories;

import com.ticketflow.api_gateway.models.client_models.tickets_api.TicketClientModel;
import com.ticketflow.api_gateway.models.movie_service.Movie;
import com.ticketflow.api_gateway.models.ticket_service.Ticket;
import com.ticketflow.api_gateway.service.convertors.ShortMovieModelConvertor;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class TicketClientModelFactory {
    private ShortMovieModelConvertor shortMovieModelConvertor;
    
    @Autowired
    public TicketClientModelFactory(ShortMovieModelConvertor shortMovieModelConvertor) {
        this.shortMovieModelConvertor = shortMovieModelConvertor;
    }

    public TicketClientModel create(Ticket ticket, Movie movie) {
        return new TicketClientModel(
            ticket.getId(),
            ticket.getBuyerEmail(),
            ticket.getRow(),
            ticket.getSeat(),
            ticket.getPrice(),
            shortMovieModelConvertor.convert(movie)
        );
    }
}