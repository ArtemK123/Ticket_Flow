package com.ticketflow.api_gateway.service;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

import com.ticketflow.api_gateway.models.client_models.movies_api.AddMovieRequestModel;
import com.ticketflow.api_gateway.models.client_models.movies_api.ShortMovieModel;
import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.movie_service.Movie;
import com.ticketflow.api_gateway.models.ticket_service.Ticket;
import com.ticketflow.api_gateway.proxy.movie.movies_api.MoviesApiProxy;
import com.ticketflow.api_gateway.proxy.ticket.TicketsApiProxy;
import com.ticketflow.api_gateway.service.convertors.ShortMovieModelConvertor;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class MoviesService {
    private static final int DEFAULT_TICKET_PRICE = 50;

    private ShortMovieModelConvertor shortMovieModelConvertor;
    private MoviesApiProxy moviesApiProxy;
    private TicketsApiProxy ticketsApiProxy;

    @Autowired
    public MoviesService(
            ShortMovieModelConvertor shortMovieModelConvertor,
            MoviesApiProxy movieServiceProxy,
            TicketsApiProxy ticketsApiProxy) {
        this.shortMovieModelConvertor = shortMovieModelConvertor;
        this.moviesApiProxy = movieServiceProxy;
        this.ticketsApiProxy = ticketsApiProxy;
    }

    public List<ShortMovieModel> getAll() {
        List<Movie> movies = moviesApiProxy.getAll();
        return movies.stream().map(movie -> shortMovieModelConvertor.convert(movie)).collect(Collectors.toList());
    }

    public Movie getById(Integer id) throws NotFoundException {
        return moviesApiProxy.getById(id);
    }

    public void add(Movie movie) throws NotFoundException {
        AddMovieRequestModel addMovieRequestModel = new AddMovieRequestModel(
            movie.getStartTime(),
            movie.getFilm().getId(),
            movie.getCinemaHall().getId()
        );

        Integer newMovieId = moviesApiProxy.add(addMovieRequestModel);
        Integer seatRows = movie.getCinemaHall().getSeatRows();
        Integer seatsInRow = movie.getCinemaHall().getSeatsInRow();
        for (int row = 1; row <= seatRows; row++) {
            for (int seat = 1; seat <= seatsInRow; seat++) {
                ticketsApiProxy.add(new Ticket(newMovieId, Optional.empty(), row, seat, DEFAULT_TICKET_PRICE));
            }
        }
    }
}