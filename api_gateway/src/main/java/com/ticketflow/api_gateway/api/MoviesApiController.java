package com.ticketflow.api_gateway.api;

import java.util.List;

import com.ticketflow.api_gateway.models.client_models.movies_api.ShortMovieModel;
import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.movie_service.Movie;
import com.ticketflow.api_gateway.service.MoviesService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class MoviesApiController {
    private MoviesService moviesService;

    @Autowired
    public MoviesApiController(MoviesService moviesService) {
        this.moviesService = moviesService;
    }

    @GetMapping(value = "/movies")
    public ResponseEntity<List<ShortMovieModel>> getAllMovies() {
        return ResponseEntity.ok(moviesService.getAllMovies());
    }

    @GetMapping(value = "/movies/{id}")
    public ResponseEntity<Movie> getMovie(@RequestParam Integer id) throws NotFoundException {
        return ResponseEntity.ok(moviesService.getMovie(id));
    }
}