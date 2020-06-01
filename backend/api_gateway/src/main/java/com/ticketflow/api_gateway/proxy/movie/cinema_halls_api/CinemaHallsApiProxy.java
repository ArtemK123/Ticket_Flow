package com.ticketflow.api_gateway.proxy.movie.cinema_halls_api;

import java.util.List;

import com.ticketflow.api_gateway.models.movie_service.CinemaHall;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class CinemaHallsApiProxy {
    private CinemaHallsApiFeignClient cinemaHallsApiFeignClient;

    @Autowired
    public CinemaHallsApiProxy(CinemaHallsApiFeignClient cinemaHallsApiFeignClient) {
        this.cinemaHallsApiFeignClient = cinemaHallsApiFeignClient;
    }

    public List<CinemaHall> getAll() {
        return cinemaHallsApiFeignClient.getAll().getBody();
    }

    public void add(CinemaHall cinemaHall) {
        cinemaHallsApiFeignClient.add(cinemaHall);
    }
}