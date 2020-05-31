package com.ticketflow.api_gateway.service.seeders;

import java.time.LocalDate;
import java.util.List;

import com.ticketflow.api_gateway.models.movie_service.Film;
import com.ticketflow.api_gateway.proxy.movie.films_api.FilmsApiProxy;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class FilmsSeeder {
    private FilmsApiProxy filmsApiProxy;
    
    @Autowired
    public FilmsSeeder(FilmsApiProxy filmsApiProxy) {
        this.filmsApiProxy = filmsApiProxy;
    }

    private List<Film> dataToSeed = List.of(
        new Film("Terminator", "I will be back", LocalDate.of(1984, 11, 26), "James Cameron", 107, 13),
        new Film("Star wars - A New Hope", "In the galaxy far far away...", LocalDate.of(1977, 5, 25), "George Lucas", 121, 8),
        new Film("Deadpool", "X gonna give it to ya", LocalDate.of(2016, 2, 8), "20th Century Fox", 108 , 18)
    );

    public void seed() {
        dataToSeed.forEach(filmsApiProxy::add);
    }
}