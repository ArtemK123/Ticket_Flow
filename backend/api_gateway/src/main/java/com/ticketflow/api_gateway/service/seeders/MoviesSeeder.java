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
    private static final int MOVIES_COUNT = 30;

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
        List<String> times = getTimes();
        List<String> dates = getDates();

        for (int i = 0; i < MOVIES_COUNT; i++) {
            Film film = allFilms.get(getRandomInt(0, allFilms.size()));
            CinemaHall cinemaHall = allCinemaHalls.get(getRandomInt(0, allCinemaHalls.size()));
            String time = times.get(getRandomInt(0, times.size()));
            String date = dates.get(getRandomInt(0, dates.size()));

            moviesService.add(new Movie(getLocalDateTime(date + " " + time), film, cinemaHall));
        }
    }

    private List<String> getTimes() {
        return List.of(
            "11:30",
            "13:00",
            "15:30",
            "17:00",
            "18:30",
            "20:00",
            "21:30"
        );
    }

    private List<String> getDates() {
        return List.of(
            "2020-06-22",
            "2020-06-23",
            "2020-06-24",
            "2020-06-25",
            "2020-06-26"
        );
    }

    private LocalDateTime getLocalDateTime(String str) {
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm");
        return LocalDateTime.parse(str, formatter);
    }

    private int getRandomInt(int from, int toNonInclusive) {
        return ThreadLocalRandom.current().nextInt(from, toNonInclusive);
    }
}