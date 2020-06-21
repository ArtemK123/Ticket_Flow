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

    private TicketsJpaRepository ticketsJpaRepository;
    private TicketConverter ticketConverter;

    @Autowired
    public TicketRepository(TicketsJpaRepository ticketsJpaRepository, TicketConverter ticketConverter) {
        this.ticketsJpaRepository = ticketsJpaRepository;
        this.ticketConverter = ticketConverter;
    }

    public Ticket getById(Integer id) throws NotFoundException {
        Optional<TicketDatabaseModel> optionalTicketDatabaseModel = ticketsJpaRepository.findById(id);

        if (optionalTicketDatabaseModel.isEmpty()) {
            throw new NotFoundException(String.format(NOT_FOUND_BY_ID_EXCEPTION_MESSAGE, id));            
        }

        return ticketConverter.convert(optionalTicketDatabaseModel.get());
    }

    public List<Ticket> getByMovie(Integer movieId) {
        List<TicketDatabaseModel> ticketDatabaseModels = ticketsJpaRepository.findByMovieId(movieId);
        return ticketDatabaseModels.stream().map(ticketConverter::convert).collect(Collectors.toList());
    }

    public List<Ticket> getByUserEmail(String email) {
        List<TicketDatabaseModel> ticketDatabaseModels = ticketsJpaRepository.findByBuyerEmail(email);
        return ticketDatabaseModels.stream().map(ticketConverter::convert).collect(Collectors.toList());
    }

    public Integer add(Ticket ticket) {
        TicketDatabaseModel ticketDatabaseModel = ticketConverter.convert(ticket);
        ticketsJpaRepository.saveAndFlush(ticketDatabaseModel);
        return ticketDatabaseModel.getId();
    }

    public void update(Integer id, Ticket ticket) throws NotFoundException {
        Optional<TicketDatabaseModel> optionalTicketDatabaseModel = ticketsJpaRepository.findById(id);

        if (optionalTicketDatabaseModel.isEmpty()) {
            throw new NotFoundException(String.format(NOT_FOUND_BY_ID_EXCEPTION_MESSAGE, id));            
        }

        TicketDatabaseModel ticketDatabaseModel = optionalTicketDatabaseModel.get();

        ticketDatabaseModel.setBuyerEmail(ticket.getBuyerEmail());
        ticketDatabaseModel.setMovieId(ticket.getMovieId());
        ticketDatabaseModel.setPrice(ticket.getPrice());
        ticketDatabaseModel.setRow(ticket.getRow());
        ticketDatabaseModel.setSeat(ticket.getSeat());

        ticketsJpaRepository.saveAndFlush(ticketDatabaseModel);
    }
}