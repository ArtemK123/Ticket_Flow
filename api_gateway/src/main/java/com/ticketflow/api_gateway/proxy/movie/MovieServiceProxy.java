package com.ticketflow.api_gateway.proxy.movie;

import java.util.List;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.movie_service.Movie;

import org.springframework.stereotype.Component;

@Component
public class MovieServiceProxy {

    public List<Movie> getAll() {
        throw new UnsupportedOperationException("MovieServiceProxy.getAll is called");
    }

    public Movie getById(Integer id) throws NotFoundException {
        throw new UnsupportedOperationException("MovieServiceProxy.getById is called");
    }
}