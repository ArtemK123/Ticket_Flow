package com.ticketflow.api_gateway.service.convertors;

import com.ticketflow.api_gateway.models.client_models.movies_api.ShortMovieModel;
import com.ticketflow.api_gateway.models.movie_service.Movie;

import org.springframework.stereotype.Component;

@Component
public class ShortMovieModelConvertor {
    public ShortMovieModel convert(Movie movie) {
        return new ShortMovieModel(
            movie.getId(),
            movie.getFilm().getTitle(),
            movie.getStartTime(),
            movie.getCinemaHall().getName());
    }
}