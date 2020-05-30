package com.ticketflow.api_gateway.service;

import java.util.List;
import java.util.stream.Collectors;

import com.ticketflow.api_gateway.models.client_models.movies_api.ShortMovieModel;
import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.movie_service.Movie;
import com.ticketflow.api_gateway.proxy.movie.MovieServiceProxy;
import com.ticketflow.api_gateway.service.convertors.ShortMovieModelConvertor;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class MoviesService {
    private ShortMovieModelConvertor shortMovieModelConvertor;
    private MovieServiceProxy movieServiceProxy;

    @Autowired
    public MoviesService(ShortMovieModelConvertor shortMovieModelConvertor, MovieServiceProxy movieServiceProxy) {
        this.shortMovieModelConvertor = shortMovieModelConvertor;
        this.movieServiceProxy = movieServiceProxy;
    }

    public List<ShortMovieModel> getAllMovies() {
        List<Movie> movies = movieServiceProxy.getAll();
        return movies.stream().map(movie -> shortMovieModelConvertor.convert(movie)).collect(Collectors.toList());
    }

    public Movie getMovie(Integer id) throws NotFoundException {
        return movieServiceProxy.getById(id);
    }
}