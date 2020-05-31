package com.ticketflow.movie_service.domain.movies;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

import com.ticketflow.movie_service.models.Movie;
import com.ticketflow.movie_service.models.exceptions.NotFoundException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

@Repository
public class MoviesRepository {
    private static final String NOT_FOUND_BY_ID_EXCEPTION_MESSAGE = "Movie with id=%d is not found";

    private MoviesJpaRepository moviesJpaRepository;
    private MovieConverter movieConverter;

    @Autowired
    public MoviesRepository(MoviesJpaRepository moviesJpaRepository, MovieConverter movieConverter) {
        this.moviesJpaRepository = moviesJpaRepository;
        this.movieConverter = movieConverter;
    }

    public List<Movie> getAll() {
        return moviesJpaRepository.findAll().stream().map(movieConverter::convert).collect(Collectors.toList());
    }

    public Movie getById(Integer id) throws NotFoundException {
        Optional<MovieDatabaseModel> optionalMovieDatabaseModel = moviesJpaRepository.findById(id);

        if (optionalMovieDatabaseModel.isEmpty()) {
            throw new NotFoundException(String.format(NOT_FOUND_BY_ID_EXCEPTION_MESSAGE, id));            
        }

        return movieConverter.convert(optionalMovieDatabaseModel.get());
    }
}