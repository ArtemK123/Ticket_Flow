package com.ticketflow.api_gateway.proxy.movie.films_api;

import java.util.List;

import com.ticketflow.api_gateway.models.movie_service.Film;
import com.ticketflow.api_gateway.proxy.movie.MovieFeignClient;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class FilmsApiProxy {
    private MovieFeignClient movieFeignClient;

    @Autowired
    public FilmsApiProxy(MovieFeignClient movieFeignClient) {
        this.movieFeignClient = movieFeignClient;
    }

    public List<Film> getAll() {
        return movieFeignClient.getAllFilms().getBody();
    }

    public void add(Film film) {
        movieFeignClient.add(film);
    }
}