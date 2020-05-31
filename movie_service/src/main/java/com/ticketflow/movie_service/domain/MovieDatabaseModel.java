package com.ticketflow.movie_service.domain;

import java.time.LocalDateTime;

import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.Table;

@Entity
@Table(name = "movies")
public class MovieDatabaseModel {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer id;

    private LocalDateTime startTime;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "films")
    private FilmDatabaseModel film;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "cinema_halls")
    private CinemaHallDatabaseModel cinemaHall;

    public Integer getId() {
        return id;
    }

    public LocalDateTime getStartTime() {
        return startTime;
    }

    public void setStartTime(LocalDateTime startTime) {
        this.startTime = startTime;
    }

    public FilmDatabaseModel getFilm() {
        return film;
    }

    public void setFilm(FilmDatabaseModel film) {
        this.film = film;
    }

    public CinemaHallDatabaseModel getCinemaHall() {
        return cinemaHall;
    }

    public void setCinemaHall(CinemaHallDatabaseModel cinemaHall) {
        this.cinemaHall = cinemaHall;
    }
}