package com.ticketflow.movie_service.models;

import java.time.LocalDateTime;

public class Movie {
    private Integer id;
    private LocalDateTime startTime;
    private Film film;
    private CinemaHall cinemaHall;

    public Movie(LocalDateTime startTime, Film film, CinemaHall cinemaHall) {
        this.startTime = startTime;
        this.film = film;
        this.cinemaHall = cinemaHall;
    }

    public Movie(Integer id, LocalDateTime startTime, Film film, CinemaHall cinemaHall) {
        this.id = id;
        this.startTime = startTime;
        this.film = film;
        this.cinemaHall = cinemaHall;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public LocalDateTime getStartTime() {
        return startTime;
    }

    public void setStartTime(LocalDateTime startTime) {
        this.startTime = startTime;
    }

    public Film getFilm() {
        return film;
    }

    public void setFilm(Film film) {
        this.film = film;
    }

    public CinemaHall getCinemaHall() {
        return cinemaHall;
    }

    public void setCinemaHall(CinemaHall cinemaHall) {
        this.cinemaHall = cinemaHall;
    }
}