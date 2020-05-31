package com.ticketflow.api_gateway.proxy.movie.movies_api;

import java.util.List;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.movie_service.Movie;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class MoviesApiProxy {
    private MoviesApiFeignClient moviesApiFeignClient;

    @Autowired
    public MoviesApiProxy(MoviesApiFeignClient movieFeignClient) {
        this.moviesApiFeignClient = movieFeignClient;
    }

    public List<Movie> getAll() {
        return moviesApiFeignClient.getAllMovies().getBody();
    }

    public Movie getById(Integer id) throws NotFoundException {
        return moviesApiFeignClient.getById(id).getBody();
    }
}