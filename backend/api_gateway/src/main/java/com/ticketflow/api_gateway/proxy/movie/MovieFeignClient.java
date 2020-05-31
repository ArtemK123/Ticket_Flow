package com.ticketflow.api_gateway.proxy.movie;

import java.util.List;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.movie_service.CinemaHall;
import com.ticketflow.api_gateway.models.movie_service.Film;
import com.ticketflow.api_gateway.models.movie_service.Movie;

import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;

@FeignClient(name = "${movie.service.name}", configuration = MovieFeignConfiguration.class)
public interface MovieFeignClient {
    @GetMapping(value = "/")
    public ResponseEntity<String> home();

    @GetMapping(value = "/movies")
    public ResponseEntity<List<Movie>> getAllMovies();

    @GetMapping(value = "/movies/{id}")
    public ResponseEntity<Movie> getById(@PathVariable Integer id) throws NotFoundException;

    @GetMapping(value = "/cinema-halls")
    public ResponseEntity<List<CinemaHall>> getAllCinemaHalls();

    @PostMapping(value = "/cinema-halls")
    public ResponseEntity<String> add(@RequestBody CinemaHall cinemaHall);

    @GetMapping(value = "/films")
    public ResponseEntity<List<Film>> getAllFilms();

    @PostMapping(value = "/films")
    public ResponseEntity<String> add(@RequestBody Film film);
}