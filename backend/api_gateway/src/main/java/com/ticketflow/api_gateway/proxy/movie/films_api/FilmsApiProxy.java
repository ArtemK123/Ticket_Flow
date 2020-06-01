package com.ticketflow.api_gateway.proxy.movie.films_api;

import java.util.List;

import com.ticketflow.api_gateway.models.movie_service.Film;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class FilmsApiProxy {
    private FilmsApiFeignClient filmsApiFeignClient;

    @Autowired
    public FilmsApiProxy(FilmsApiFeignClient filmsApiFeignClient) {
        this.filmsApiFeignClient = filmsApiFeignClient;
    }

    public List<Film> getAll() {
        return filmsApiFeignClient.getAll().getBody();
    }

    public void add(Film film) {
        filmsApiFeignClient.add(film);
    }
}