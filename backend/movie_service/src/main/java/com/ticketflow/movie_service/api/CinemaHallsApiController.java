package com.ticketflow.movie_service.api;

import java.util.List;

import com.ticketflow.movie_service.models.CinemaHall;
import com.ticketflow.movie_service.service.CinemaHallsService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class CinemaHallsApiController {
    private CinemaHallsService cinemaHallsService;
    
    @Autowired
    public CinemaHallsApiController(CinemaHallsService cinemaHallsService) {
        this.cinemaHallsService = cinemaHallsService;
    }

    @GetMapping(value = "/cinema-halls")
    public ResponseEntity<List<CinemaHall>> getAll() {
        return ResponseEntity.ok(cinemaHallsService.getAll());
    }

    @PostMapping(value = "/cinema-halls")
    public ResponseEntity<Integer> add(@RequestBody CinemaHall cinemaHall) {
        return ResponseEntity.status(201).body(cinemaHallsService.add(cinemaHall));
    }
}