package com.ticketflow.movie_service.domain.movies;

import com.ticketflow.movie_service.domain.cinema_halls.CinemaHallConverter;
import com.ticketflow.movie_service.domain.films.FilmConverter;
import com.ticketflow.movie_service.models.Movie;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class MovieConverter {
    private FilmConverter filmConverter;
    private CinemaHallConverter cinemaHallConverter;
    
    @Autowired
    public MovieConverter(FilmConverter filmConverter, CinemaHallConverter cinemaHallConverter) {
        this.filmConverter = filmConverter;
        this.cinemaHallConverter = cinemaHallConverter;
    }

    public Movie convert(MovieDatabaseModel movieDatabaseModel) {
        return new Movie(
            movieDatabaseModel.getId(),
            movieDatabaseModel.getStartTime(),
            filmConverter.convert(movieDatabaseModel.getFilm()),
            cinemaHallConverter.convert(movieDatabaseModel.getCinemaHall())
        );
    }

    public MovieDatabaseModel convert(Movie film) {
        return new MovieDatabaseModel(
            film.getStartTime(),
            filmConverter.convert(film.getFilm()),
            cinemaHallConverter.convert(film.getCinemaHall())
        );
    }
}