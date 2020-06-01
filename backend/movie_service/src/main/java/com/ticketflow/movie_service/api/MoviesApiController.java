package com.ticketflow.movie_service.api;

import java.util.List;

import com.ticketflow.movie_service.models.Movie;
import com.ticketflow.movie_service.models.client_models.AddMovieRequestModel;
import com.ticketflow.movie_service.models.exceptions.NotFoundException;
import com.ticketflow.movie_service.service.MoviesService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class MoviesApiController {
    private MoviesService moviesService;

    @Autowired
    public MoviesApiController(MoviesService moviesService) {
        this.moviesService = moviesService;
    }

    @GetMapping(value = "/movies")
    public ResponseEntity<List<Movie>> getAll() {
        return ResponseEntity.ok(moviesService.getAll());
    }

    @PostMapping(value = "/movies")
    public ResponseEntity<Integer> add(@RequestBody AddMovieRequestModel addMovieRequestModel) throws NotFoundException {
        return ResponseEntity.status(201).body(moviesService.add(addMovieRequestModel));
    }

    @GetMapping(value = "/movies/{id}")
    public ResponseEntity<Movie> getById(@PathVariable Integer id) throws NotFoundException {
        return ResponseEntity.ok(moviesService.getById(id));
    }
}