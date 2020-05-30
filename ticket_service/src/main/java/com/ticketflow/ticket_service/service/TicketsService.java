package com.ticketflow.ticket_service.service;

import java.util.List;

import com.ticketflow.ticket_service.domain.TicketRepository;
import com.ticketflow.ticket_service.models.Ticket;
import com.ticketflow.ticket_service.models.client_models.OrderRequestModel;
import com.ticketflow.ticket_service.models.exceptions.NotFoundException;
import com.ticketflow.ticket_service.models.exceptions.TicketAlreadyOrderedException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class TicketsService {
    private static final String TICKET_ALREADY_ORDERED_EXCEPTION_MESSAGE = "Ticket with id=%d is already ordered";

    private TicketRepository ticketRepository;

    @Autowired
    public TicketsService(TicketRepository ticketRepository) {
        this.ticketRepository = ticketRepository;
    }

    public List<Ticket> getByMovieId(Integer movieId) {
        return ticketRepository.getByMovie(movieId);
    }

    public List<Ticket> getByBuyerEmail(String email) {
        return ticketRepository.getByBuyerEmail(email);
    }

    public void order(OrderRequestModel orderRequestModel) throws TicketAlreadyOrderedException, NotFoundException {
        Ticket ticket = ticketRepository.getById(orderRequestModel.getTicketId());
        if (ticket.getBuyerEmail() != null) {
            throw new TicketAlreadyOrderedException(String.format(TICKET_ALREADY_ORDERED_EXCEPTION_MESSAGE, orderRequestModel.getTicketId()));
        }
        ticket.setBuyerEmail(orderRequestModel.getBuyerEmail());
    }
}