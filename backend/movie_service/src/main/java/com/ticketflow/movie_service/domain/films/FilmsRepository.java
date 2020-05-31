package com.ticketflow.movie_service.domain.films;

import java.util.List;
import java.util.stream.Collectors;

import com.ticketflow.movie_service.models.Film;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

@Repository
public class FilmsRepository {
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

    public Integer add(Film film) {
        FilmDatabaseModel filmDatabaseModel = filmConverter.convert(film);
        filmsJpaRepository.saveAndFlush(filmDatabaseModel);
        return filmDatabaseModel.getId();
    }
}