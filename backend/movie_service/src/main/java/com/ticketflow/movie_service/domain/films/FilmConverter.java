package com.ticketflow.movie_service.domain.films;

import com.ticketflow.movie_service.models.Film;

import org.springframework.stereotype.Component;

@Component
public class FilmConverter {
    public Film convert(FilmDatabaseModel filmDatabaseModel) {
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

    public FilmDatabaseModel convert(Film film) {
        return new FilmDatabaseModel(
            film.getTitle(),
            film.getDescription(),
            film.getPremiereDate(),
            film.getCreator(),
            film.getDuration(),
            film.getAgeLimit()
        );
    }
}