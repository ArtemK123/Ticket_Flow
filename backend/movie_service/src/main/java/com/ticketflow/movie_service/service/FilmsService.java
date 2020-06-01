package com.ticketflow.movie_service.service;

import java.util.List;

import com.ticketflow.movie_service.domain.films.FilmsRepository;
import com.ticketflow.movie_service.models.Film;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class FilmsService {
    private FilmsRepository filmsRepository;

    @Autowired
    public FilmsService(FilmsRepository filmsRepository) {
        this.filmsRepository = filmsRepository;
    }

    public List<Film> getAll() {
        return filmsRepository.getAll();
    }
    
    public Integer add(Film film) {
        return filmsRepository.add(film);
    }
}