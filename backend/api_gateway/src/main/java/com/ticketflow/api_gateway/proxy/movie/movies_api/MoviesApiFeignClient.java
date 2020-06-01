package com.ticketflow.api_gateway.proxy.movie.movies_api;

import java.util.List;

import com.ticketflow.api_gateway.models.client_models.movies_api.AddMovieRequestModel;
import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.movie_service.Movie;
import com.ticketflow.api_gateway.proxy.movie.MovieFeignConfiguration;

import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;

@FeignClient(contextId = "MoviesApiFeignClient", name = "${movie.service.name}", configuration = MovieFeignConfiguration.class)
public interface MoviesApiFeignClient {
    @GetMapping(value = "/")
    public ResponseEntity<String> home();

    @GetMapping(value = "/movies")
    public ResponseEntity<List<Movie>> getAllMovies();

    @GetMapping(value = "/movies/{id}")
    public ResponseEntity<Movie> getById(@PathVariable Integer id) throws NotFoundException;

    @PostMapping(value = "/movies")
    public ResponseEntity<Integer> add(AddMovieRequestModel addMovieRequestModel) throws NotFoundException;
}