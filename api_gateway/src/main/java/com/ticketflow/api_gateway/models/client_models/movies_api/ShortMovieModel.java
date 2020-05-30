package com.ticketflow.api_gateway.models.client_models.movies_api;

import java.time.LocalDateTime;

public class ShortMovieModel {
    private Integer id;
    private String title;
    private LocalDateTime startTime;
    private String cinemaHallName;

    public ShortMovieModel(Integer id, String title, LocalDateTime startTime, String cinemaHallName) {
        this.id = id;
        this.title = title;
        this.startTime = startTime;
        this.cinemaHallName = cinemaHallName;
    }

    public Integer getId() {
        return id;
    }

    public String getTitle() {
        return title;
    }

    public LocalDateTime getStartTime() {
        return startTime;
    }

    public String getCinemaHallName() {
        return cinemaHallName;
    }
}