package com.ticketflow.movie_service.domain.films;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

import com.ticketflow.movie_service.models.Film;
import com.ticketflow.movie_service.models.exceptions.NotFoundException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

@Repository
public class FilmsRepository {
    private static final String NOT_FOUND_BY_ID_EXCEPTION_MESSAGE = "Film with id=%d is not found";

    private FilmsJpaRepository filmsJpaRepository;
    private FilmConverter filmConverter;

    @Autowired
    public FilmsRepository(FilmsJpaRepository filmsJpaRepository, FilmConverter filmConverter) {
        this.filmsJpaRepository = filmsJpaRepository;
        this.filmConverter = filmConverter;
    }

    public List<Film> getAll() {
        return filmsJpaRepository.findAll().stream().map(filmConverter::convert).collect(Collectors.toList());
    }

    public Film getById(Integer id) throws NotFoundException {
        Optional<FilmDatabaseModel> optionalFilmDatabaseModel = filmsJpaRepository.findById(id);

        if (optionalFilmDatabaseModel.isEmpty()) {
            throw new NotFoundException(String.format(NOT_FOUND_BY_ID_EXCEPTION_MESSAGE, id));            
        }

        return filmConverter.convert(optionalFilmDatabaseModel.get());
    }

    public Integer add(Film film) {
        FilmDatabaseModel filmDatabaseModel = filmConverter.convert(film);
        filmsJpaRepository.saveAndFlush(filmDatabaseModel);
        return filmDatabaseModel.getId();
    }
}