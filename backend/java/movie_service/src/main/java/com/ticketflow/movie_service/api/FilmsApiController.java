package com.ticketflow.movie_service.api;

import java.util.List;

import com.ticketflow.movie_service.models.Film;
import com.ticketflow.movie_service.service.FilmsService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class FilmsApiController {
    private FilmsService filmsService;
    
    @Autowired
    public FilmsApiController(FilmsService filmsService) {
        this.filmsService = filmsService;
    }

    @GetMapping(value = "/films")
    public ResponseEntity<List<Film>> getAll() {
        return ResponseEntity.ok(filmsService.getAll());
    }

    @PostMapping(value = "/films")
    public ResponseEntity<Integer> add(@RequestBody Film film) {
        return ResponseEntity.status(201).body(filmsService.add(film));
    }
}