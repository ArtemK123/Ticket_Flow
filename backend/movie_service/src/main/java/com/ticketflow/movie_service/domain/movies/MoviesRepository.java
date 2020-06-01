package com.ticketflow.movie_service.domain.movies;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

import com.ticketflow.movie_service.domain.cinema_halls.CinemaHallDatabaseModel;
import com.ticketflow.movie_service.domain.cinema_halls.CinemaHallsJpaRepository;
import com.ticketflow.movie_service.domain.films.FilmDatabaseModel;
import com.ticketflow.movie_service.domain.films.FilmsJpaRepository;
import com.ticketflow.movie_service.models.Movie;
import com.ticketflow.movie_service.models.client_models.AddMovieRequestModel;
import com.ticketflow.movie_service.models.exceptions.NotFoundException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

@Repository
public class MoviesRepository {
    private static final String MOVIE_NOT_FOUND_BY_ID_EXCEPTION_MESSAGE = "Movie with id=%d is not found";
    private static final String FILM_NOT_FOUND_BY_ID_EXCEPTION_MESSAGE = "Film with id=%d is not found";
    private static final String CINEMA_HALL_NOT_FOUND_BY_ID_EXCEPTION_MESSAGE = "Cinema hall with id=%d is not found";

    private MoviesJpaRepository moviesJpaRepository;
    private FilmsJpaRepository filmsJpaRepository;
    private CinemaHallsJpaRepository cinemaHallsJpaRepository;
    private MovieConverter movieConverter;

    @Autowired
    public MoviesRepository(
            MoviesJpaRepository moviesJpaRepository,
            MovieConverter movieConverter,
            FilmsJpaRepository filmsJpaRepository,
            CinemaHallsJpaRepository cinemaHallsJpaRepository
            ) {
        this.moviesJpaRepository = moviesJpaRepository;
        this.movieConverter = movieConverter;
        this.filmsJpaRepository = filmsJpaRepository;
        this.cinemaHallsJpaRepository = cinemaHallsJpaRepository;
    }

    public List<Movie> getAll() {
        return moviesJpaRepository.findAll().stream().map(movieConverter::convert).collect(Collectors.toList());
    }

    public Movie getById(Integer id) throws NotFoundException {
        Optional<MovieDatabaseModel> optionalMovieDatabaseModel = moviesJpaRepository.findById(id);

        if (optionalMovieDatabaseModel.isEmpty()) {
            throw new NotFoundException(String.format(MOVIE_NOT_FOUND_BY_ID_EXCEPTION_MESSAGE, id));            
        }

        return movieConverter.convert(optionalMovieDatabaseModel.get());
    }

    public Integer add(AddMovieRequestModel addMovieRequestModel) throws NotFoundException {
        Optional<FilmDatabaseModel> optionalFilmDatabaseModel = filmsJpaRepository.findById(addMovieRequestModel.getFilmId());
        Optional<CinemaHallDatabaseModel> optionalCinemaHallDatabaseModel = cinemaHallsJpaRepository.findById(addMovieRequestModel.getCinemaHallId());

        if (optionalFilmDatabaseModel.isEmpty()) {
            throw new NotFoundException(String.format(FILM_NOT_FOUND_BY_ID_EXCEPTION_MESSAGE, addMovieRequestModel.getFilmId()));
        }

        if (optionalCinemaHallDatabaseModel.isEmpty()) {
            throw new NotFoundException(String.format(CINEMA_HALL_NOT_FOUND_BY_ID_EXCEPTION_MESSAGE, addMovieRequestModel.getCinemaHallId()));
        }
        MovieDatabaseModel movieDatabaseModel = new MovieDatabaseModel(
            addMovieRequestModel.getStartTime(),
            optionalFilmDatabaseModel.get(),
            optionalCinemaHallDatabaseModel.get());

        moviesJpaRepository.saveAndFlush(movieDatabaseModel);
        return movieDatabaseModel.getId();
    }
}