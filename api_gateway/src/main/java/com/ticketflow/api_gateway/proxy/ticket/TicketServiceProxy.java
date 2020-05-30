package com.ticketflow.api_gateway.proxy.ticket;

import java.util.List;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.ticket_service.Ticket;
import com.ticketflow.api_gateway.models.ticket_service.exceptions.TicketAlreadyOrderedException;

import org.springframework.stereotype.Component;

@Component
public class TicketServiceProxy {

    public List<Ticket> getByMovie(Integer movieId) throws NotFoundException {
        throw new UnsupportedOperationException("TicketServiceProxy.getByMovie is called");
    }

    public List<Ticket> getByUserEmail(String userEmail) throws NotFoundException {
        throw new UnsupportedOperationException("TicketServiceProxy.getByUserEmail is called");
    }

    public String order(Integer ticketId, String userEmail) throws NotFoundException, TicketAlreadyOrderedException {
        throw new UnsupportedOperationException("TicketServiceProxy.order is called");
    }
}