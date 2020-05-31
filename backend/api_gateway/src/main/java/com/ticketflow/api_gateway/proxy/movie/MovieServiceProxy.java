package com.ticketflow.api_gateway.proxy.movie;

import java.util.List;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.movie_service.Movie;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class MovieServiceProxy {

    private MoviesFeignClient moviesFeignClient;

    @Autowired
    public MovieServiceProxy(MoviesFeignClient moviesFeignClient) {
        this.moviesFeignClient = moviesFeignClient;
    }

    public List<Movie> getAll() {
        return moviesFeignClient.getAll().getBody();
    }

    public Movie getById(Integer id) throws NotFoundException {
        return moviesFeignClient.getById(id).getBody();
    }
}