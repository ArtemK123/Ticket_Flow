package com.ticketflow.movie_service.domain;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

import com.ticketflow.movie_service.models.CinemaHall;
import com.ticketflow.movie_service.models.Film;
import com.ticketflow.movie_service.models.Movie;
import com.ticketflow.movie_service.models.exceptions.NotFoundException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

@Repository
public class MoviesRepository {
    private static final String NOT_FOUND_BY_ID_EXCEPTION_MESSAGE = "Movie with id=%d is not found";

    private MoviesJpaRepository moviesJpaRepository;

    @Autowired
    public MoviesRepository(MoviesJpaRepository moviesJpaRepository) {
        this.moviesJpaRepository = moviesJpaRepository;
    }

    public List<Movie> getAll() {
        return moviesJpaRepository.findAll().stream().map(this::convertToMovie).collect(Collectors.toList());
    }

    public Movie getById(Integer id) throws NotFoundException {
        Optional<MovieDatabaseModel> optionalMovieDatabaseModel = moviesJpaRepository.findById(id);

        if (optionalMovieDatabaseModel.isEmpty()) {
            throw new NotFoundException(String.format(NOT_FOUND_BY_ID_EXCEPTION_MESSAGE, id));            
        }

        return convertToMovie(optionalMovieDatabaseModel.get());
    }

    private Movie convertToMovie(MovieDatabaseModel movieDatabaseModel) {
        return new Movie(
            movieDatabaseModel.getId(),
            movieDatabaseModel.getStartTime(),
            convertToFilm(movieDatabaseModel.getFilm()),
            convertToCinemaHall(movieDatabaseModel.getCinemaHall())
        );
    }

    private Film convertToFilm(FilmDatabaseModel filmDatabaseModel) {
        return new Film(
            filmDatabaseModel.getId(),
            filmDatabaseModel.getTitle(),
            filmDatabaseModel.getDescription(),
            filmDatabaseModel.getPremiereDate(),
            filmDatabaseModel.getCreator(),
            filmDatabaseModel.getDuration(),
            filmDatabaseModel.getAgeLimit()
        );
    }

    private CinemaHall convertToCinemaHall(CinemaHallDatabaseModel cinemaHallDatabaseModel) {
        return new CinemaHall(
            cinemaHallDatabaseModel.getId(),
            cinemaHallDatabaseModel.getName(),
            cinemaHallDatabaseModel.getLocation(),
            cinemaHallDatabaseModel.getCapacity());
    }
}