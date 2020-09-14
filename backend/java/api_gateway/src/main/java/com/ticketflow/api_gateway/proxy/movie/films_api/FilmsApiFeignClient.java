package com.ticketflow.api_gateway.proxy.movie.films_api;

import java.util.List;

import com.ticketflow.api_gateway.models.movie_service.Film;
import com.ticketflow.api_gateway.proxy.movie.MovieFeignConfiguration;

import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;

@FeignClient(contextId = "FilmsApiFeignClient", name = "${movie.service.name}", configuration = MovieFeignConfiguration.class)
public interface FilmsApiFeignClient {
    @GetMapping(value = "/films")
    public ResponseEntity<List<Film>> getAll();

    @PostMapping(value = "/films")
    public ResponseEntity<Integer> add(@RequestBody Film cinemaHall);
}