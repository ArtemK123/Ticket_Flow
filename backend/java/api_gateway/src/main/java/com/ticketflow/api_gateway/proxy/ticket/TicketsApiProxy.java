package com.ticketflow.api_gateway.proxy.ticket;

import java.util.List;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.ticket_service.OrderModel;
import com.ticketflow.api_gateway.models.ticket_service.Ticket;
import com.ticketflow.api_gateway.models.ticket_service.exceptions.TicketAlreadyOrderedException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class TicketsApiProxy {
    private TicketsApiFeignClient ticketsFeignClient;

    @Autowired
    public TicketsApiProxy(TicketsApiFeignClient ticketsFeignClient) {
        this.ticketsFeignClient = ticketsFeignClient;
    }

    public List<Ticket> getByMovieId(Integer movieId) {
        return ticketsFeignClient.getByMovieId(movieId).getBody();
    }

    public List<Ticket> getByUserEmail(String userEmail) {
        return ticketsFeignClient.getByUserEmail(userEmail).getBody();
    }

    public void add(Ticket ticket){
        ticketsFeignClient.add(ticket);
    }

    public void order(OrderModel orderModel)
            throws TicketAlreadyOrderedException, NotFoundException {
        ticketsFeignClient.order(orderModel);
    }
}