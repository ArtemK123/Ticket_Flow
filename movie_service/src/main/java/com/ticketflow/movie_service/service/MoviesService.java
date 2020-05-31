package com.ticketflow.movie_service.service;

import java.util.List;

import com.ticketflow.movie_service.domain.MoviesRepository;
import com.ticketflow.movie_service.models.Movie;
import com.ticketflow.movie_service.models.exceptions.NotFoundException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class MoviesService {
    private MoviesRepository moviesRepository;

    @Autowired
    public MoviesService(MoviesRepository moviesRepository) {
        this.moviesRepository = moviesRepository;
    }

    public List<Movie> getAll() {
        return moviesRepository.getAll();
    }

    public Movie getById(Integer id) throws NotFoundException {
        return moviesRepository.getById(id);
    }
}