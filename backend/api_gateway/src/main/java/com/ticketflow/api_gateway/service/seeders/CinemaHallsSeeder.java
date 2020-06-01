package com.ticketflow.api_gateway.service.seeders;

import java.util.List;

import com.ticketflow.api_gateway.models.movie_service.CinemaHall;
import com.ticketflow.api_gateway.proxy.movie.cinema_halls_api.CinemaHallsApiProxy;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class CinemaHallsSeeder {
    private List<CinemaHall> dataToSeed = List.of(
        new CinemaHall("SuperLux", "Kyiv", 5, 5),
        new CinemaHall("JustRelax", "Kyiv", 5, 5),
        new CinemaHall("RedHall", "Lviv", 5, 5)
    );

    private CinemaHallsApiProxy cinemaHallsApiProxy;
    
    @Autowired
    public CinemaHallsSeeder(CinemaHallsApiProxy cinemaHallsApiProxy) {
        this.cinemaHallsApiProxy = cinemaHallsApiProxy;
    }

    public void seed() {
        if (cinemaHallsApiProxy.getAll().isEmpty()) {
            dataToSeed.forEach(cinemaHallsApiProxy::add);
        }
    }
}