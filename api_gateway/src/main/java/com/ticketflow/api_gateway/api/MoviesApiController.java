package com.ticketflow.api_gateway.api;

import java.util.List;

import com.ticketflow.api_gateway.models.client_models.movies_api.ShortMovieDescription;
import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.movie_service.Movie;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class MoviesApiController {
    @GetMapping(value = "/movies")
    public ResponseEntity<List<ShortMovieDescription>> getAllMovies() {
        throw new UnsupportedOperationException("MoviesApiController.getAllMovies is called");
    }

    @GetMapping(value = "/movies/{id}")
    public ResponseEntity<Movie> getMovie(@RequestParam Integer id) throws NotFoundException {
        throw new UnsupportedOperationException("MoviesApiController.getMovie is called");
    }
}