package com.ticketflow.api_gateway.proxy.movie.cinema_halls_api;

import java.util.List;

import com.ticketflow.api_gateway.models.movie_service.CinemaHall;
import com.ticketflow.api_gateway.proxy.movie.MovieFeignClient;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class CinemaHallsApiProxy {
    private MovieFeignClient movieFeignClient;

    @Autowired
    public CinemaHallsApiProxy(MovieFeignClient movieFeignClient) {
        this.movieFeignClient = movieFeignClient;
    }

    public List<CinemaHall> getAll() {
        return movieFeignClient.getAllCinemaHalls().getBody();
    }

    public void add(CinemaHall cinemaHall) {
        movieFeignClient.add(cinemaHall);
    }
}