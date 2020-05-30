package com.ticketflow.ticket_service.domain;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

import com.ticketflow.ticket_service.models.Ticket;
import com.ticketflow.ticket_service.models.exceptions.NotFoundException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

@Repository
public class TicketRepository {
    private static final String NOT_FOUND_BY_ID_EXCEPTION_MESSAGE = "Ticket with id=%d is not found";

    private TicketJpaRepository ticketJpaRepository;

    @Autowired
    public TicketRepository(TicketJpaRepository ticketJpaRepository) {
        this.ticketJpaRepository = ticketJpaRepository;
    }

    public Ticket getById(Integer id) throws NotFoundException {
        Optional<TicketDatabaseModel> optionalTicketDatabaseModel = ticketJpaRepository.findById(id);

        if (optionalTicketDatabaseModel.isEmpty()) {
            throw new NotFoundException(String.format(NOT_FOUND_BY_ID_EXCEPTION_MESSAGE, id));            
        }

        return convertToTicket(optionalTicketDatabaseModel.get());
    }

    public List<Ticket> getByMovie(Integer movieId) {
        List<TicketDatabaseModel> ticketDatabaseModels = ticketJpaRepository.findByMovieId(movieId);
        return ticketDatabaseModels.stream().map(this::convertToTicket).collect(Collectors.toList());
    }

    public List<Ticket> getByBuyerEmail(String email) {
        List<TicketDatabaseModel> ticketDatabaseModels = ticketJpaRepository.findByBuyerEmail(email);
        return ticketDatabaseModels.stream().map(this::convertToTicket).collect(Collectors.toList());
    }

    public void update(Integer id, Ticket ticket) throws NotFoundException {
        Optional<TicketDatabaseModel> optionalTicketDatabaseModel = ticketJpaRepository.findById(id);

        if (optionalTicketDatabaseModel.isEmpty()) {
            throw new NotFoundException(String.format(NOT_FOUND_BY_ID_EXCEPTION_MESSAGE, id));            
        }

        TicketDatabaseModel ticketDatabaseModel = optionalTicketDatabaseModel.get();

        ticketDatabaseModel.setBuyerEmail(ticket.getBuyerEmail());
        ticketDatabaseModel.setMovieId(ticket.getMovieId());
        ticketDatabaseModel.setPrice(ticket.getPrice());
        ticketDatabaseModel.setRow(ticket.getRow());
        ticketDatabaseModel.setSeat(ticket.getRow());

        ticketJpaRepository.saveAndFlush(ticketDatabaseModel);
    }

    private Ticket convertToTicket(TicketDatabaseModel ticketDatabaseModel) {
        return new Ticket(
            ticketDatabaseModel.getId(),
            ticketDatabaseModel.getMovieId(),
            ticketDatabaseModel.getBuyerEmail(),
            ticketDatabaseModel.getRow(),
            ticketDatabaseModel.getSeat(),
            ticketDatabaseModel.getPrice()
        );
    }
}