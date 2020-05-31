package com.ticketflow.api_gateway.proxy.movie.movies_api;

import java.util.List;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.movie_service.Movie;
import com.ticketflow.api_gateway.proxy.movie.MovieFeignClient;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class MoviesApiProxy {
    private MovieFeignClient movieFeignClient;

    @Autowired
    public MoviesApiProxy(MovieFeignClient movieFeignClient) {
        this.movieFeignClient = movieFeignClient;
    }

    public List<Movie> getAll() {
        return movieFeignClient.getAllMovies().getBody();
    }

    public Movie getById(Integer id) throws NotFoundException {
        return movieFeignClient.getById(id).getBody();
    }
}