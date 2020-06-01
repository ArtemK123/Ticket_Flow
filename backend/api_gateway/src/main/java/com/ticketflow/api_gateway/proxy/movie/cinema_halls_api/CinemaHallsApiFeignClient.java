package com.ticketflow.api_gateway.proxy.movie.cinema_halls_api;

import java.util.List;

import com.ticketflow.api_gateway.models.movie_service.CinemaHall;
import com.ticketflow.api_gateway.proxy.movie.MovieFeignConfiguration;

import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;

@FeignClient(contextId = "CinemaHallsApiFeignClient", name = "${movie.service.name}", configuration = MovieFeignConfiguration.class)
public interface CinemaHallsApiFeignClient {
    @GetMapping(value = "/cinema-halls")
    public ResponseEntity<List<CinemaHall>> getAll();

    @PostMapping(value = "/cinema-halls")
    public ResponseEntity<Integer> add(@RequestBody CinemaHall cinemaHall);
}