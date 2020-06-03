package com.ticketflow.api_gateway.service.seeders;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.List;
import java.util.concurrent.ThreadLocalRandom;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.movie_service.CinemaHall;
import com.ticketflow.api_gateway.models.movie_service.Film;
import com.ticketflow.api_gateway.models.movie_service.Movie;
import com.ticketflow.api_gateway.proxy.movie.cinema_halls_api.CinemaHallsApiProxy;
import com.ticketflow.api_gateway.proxy.movie.films_api.FilmsApiProxy;
import com.ticketflow.api_gateway.service.MoviesService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class MoviesSeeder {
    private FilmsApiProxy filmsApiProxy;
    private CinemaHallsApiProxy cinemaHallsApiProxy;
    private MoviesService moviesService;

    @Autowired
    public MoviesSeeder(FilmsApiProxy filmsApiProxy, CinemaHallsApiProxy cinemaHallsApiProxy,
            MoviesService moviesService) {
        this.filmsApiProxy = filmsApiProxy;
        this.cinemaHallsApiProxy = cinemaHallsApiProxy;
        this.moviesService = moviesService;
    }

    public void seed() throws NotFoundException {
        if (!moviesService.getAll().isEmpty()) {
            return;
        }

        List<CinemaHall> allCinemaHalls = cinemaHallsApiProxy.getAll();
        List<Film> allFilms = filmsApiProxy.getAll();
        List<LocalDateTime> startTimes = getStartTimes();

        for (Film film : allFilms) {
            for (LocalDateTime startTime : startTimes) {
                int cinemaHallIndex = ThreadLocalRandom.current().nextInt(0, allCinemaHalls.size());
                CinemaHall cinemaHall = allCinemaHalls.get(cinemaHallIndex);
                moviesService.add(new Movie(startTime, film, cinemaHall));
            }
        }
    }

    private List<LocalDateTime> getStartTimes() {
        return List.of(
            getLocalDateTime("2020-06-10 11:30"),
            getLocalDateTime("2020-06-11 13:00"),
            getLocalDateTime("2020-06-12 17:00")
        );
    }

    private LocalDateTime getLocalDateTime(String str) {
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm");
        return LocalDateTime.parse(str, formatter);
    }
}