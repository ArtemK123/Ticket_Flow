package com.ticketflow.movie_service.models.client_models;

import java.time.LocalDateTime;

public class AddMovieRequestModel {
    private LocalDateTime startTime;
    private Integer filmId;
    private Integer cinemaHallId;

    public AddMovieRequestModel(LocalDateTime startTime, Integer filmId, Integer cinemaHallId) {
        this.startTime = startTime;
        this.filmId = filmId;
        this.cinemaHallId = cinemaHallId;
    }

    public LocalDateTime getStartTime() {
        return startTime;
    }

    public Integer getFilmId() {
        return filmId;
    }

    public Integer getCinemaHallId() {
        return cinemaHallId;
    }
}