package com.ticketflow.api_gateway.proxy.movie;

import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;

import java.util.List;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.movie_service.Movie;

@FeignClient(name = "${movie.service.name}", configuration = MovieFeignConfiguration.class)
interface MoviesFeignClient {
    @GetMapping(value = "/")
    public ResponseEntity<String> home();
    
    @GetMapping(value = "/movies")
    public ResponseEntity<List<Movie>> getAll();

    @GetMapping(value = "/movies/{id}")
    public ResponseEntity<Movie> getById(@PathVariable Integer id) throws NotFoundException;
}