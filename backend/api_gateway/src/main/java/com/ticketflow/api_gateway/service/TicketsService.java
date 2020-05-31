package com.ticketflow.api_gateway.service;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

import com.ticketflow.api_gateway.models.client_models.tickets_api.OrderRequestModel;
import com.ticketflow.api_gateway.models.client_models.tickets_api.TicketClientModel;
import com.ticketflow.api_gateway.models.exceptions.InvalidTokenException;
import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.identity_service.User;
import com.ticketflow.api_gateway.models.movie_service.Movie;
import com.ticketflow.api_gateway.models.ticket_service.OrderModel;
import com.ticketflow.api_gateway.models.ticket_service.Ticket;
import com.ticketflow.api_gateway.models.ticket_service.exceptions.TicketAlreadyOrderedException;
import com.ticketflow.api_gateway.proxy.identity.IdentityServiceProxy;
import com.ticketflow.api_gateway.proxy.movie.MovieServiceProxy;
import com.ticketflow.api_gateway.proxy.ticket.TicketServiceProxy;
import com.ticketflow.api_gateway.service.factories.TicketClientModelFactory;
import com.ticketflow.api_gateway.service.validators.TokenValidator;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class TicketsService {
    private static final String USER_WITH_TOKEN_NOT_FOUND_EXCEPTION_MESSAGE = "User with given token is not found";

    private IdentityServiceProxy identityServiceProxy;
    private TicketServiceProxy ticketServiceProxy;
    private MovieServiceProxy movieServiceProxy;
    private TicketClientModelFactory ticketClientModelFactory;
    private TokenValidator tokenValidator;

    @Autowired
    public TicketsService(
            IdentityServiceProxy identityServiceProxy,
            TicketServiceProxy ticketServiceProxy,
            MovieServiceProxy movieServiceProxy,
            TicketClientModelFactory ticketClientModelFactory,
            TokenValidator tokenValidator) {
        this.identityServiceProxy = identityServiceProxy;
        this.ticketServiceProxy = ticketServiceProxy;
        this.movieServiceProxy = movieServiceProxy;
        this.ticketClientModelFactory = ticketClientModelFactory;
        this.tokenValidator = tokenValidator;
    }

    public List<TicketClientModel> getTicketsByMovie(Integer id) throws NotFoundException {
        Movie movie = movieServiceProxy.getById(id);
        List<Ticket> tickets = ticketServiceProxy.getByMovieId(id);
        return tickets.stream().map(ticket -> ticketClientModelFactory.create(ticket, movie)).collect(Collectors.toList());
    }

    public List<TicketClientModel> getTicketsByUser(String token) throws InvalidTokenException, NotFoundException {
        tokenValidator.validate(token);
        
        User user;
        try {
            user = identityServiceProxy.getByToken(token);
        } catch (NotFoundException exception) {
            throw new InvalidTokenException(USER_WITH_TOKEN_NOT_FOUND_EXCEPTION_MESSAGE);
        }

        List<Ticket> tickets = ticketServiceProxy.getByUserEmail(user.getEmail());
        if (tickets.isEmpty()) {
            return new ArrayList<>();
        }

        Movie movie = movieServiceProxy.getById(tickets.get(0).getMovieId());
        return tickets.stream().map(ticket -> ticketClientModelFactory.create(ticket, movie)).collect(Collectors.toList());
    }

    public void order(OrderRequestModel orderRequestModel) throws TicketAlreadyOrderedException, InvalidTokenException, NotFoundException {
        User user;
        try {
            user = identityServiceProxy.getByToken(orderRequestModel.getToken());
        } catch (NotFoundException exception) {
            throw new InvalidTokenException(USER_WITH_TOKEN_NOT_FOUND_EXCEPTION_MESSAGE);
        }

        ticketServiceProxy.order(new OrderModel(orderRequestModel.getTicketId(), user.getEmail()));
    }
}