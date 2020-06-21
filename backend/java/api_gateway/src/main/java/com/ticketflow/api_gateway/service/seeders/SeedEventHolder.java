package com.ticketflow.api_gateway.service.seeders;

import org.springframework.beans.factory.InitializingBean;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class SeedEventHolder implements InitializingBean {
    private FilmsSeeder filmsSeeder;
    private CinemaHallsSeeder cinemaHallsSeeder;
    private MoviesSeeder moviesSeeder;

    @Autowired
    public SeedEventHolder(FilmsSeeder filmsSeeder, CinemaHallsSeeder cinemaHallsSeeder, MoviesSeeder moviesSeeder) {
        this.filmsSeeder = filmsSeeder;
        this.cinemaHallsSeeder = cinemaHallsSeeder;
        this.moviesSeeder = moviesSeeder;
    }

    @Override
    public void afterPropertiesSet() throws Exception {
        filmsSeeder.seed();
        cinemaHallsSeeder.seed();
        moviesSeeder.seed();
    }
}