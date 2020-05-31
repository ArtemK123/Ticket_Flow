package com.ticketflow.api_gateway.service.seeders;

import org.springframework.beans.factory.InitializingBean;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class SeedEventHolder implements InitializingBean {
    private FilmsSeeder filmsSeeder;
    private CinemaHallsSeeder cinemaHallsSeeder;

    @Autowired
    public SeedEventHolder(FilmsSeeder filmsSeeder, CinemaHallsSeeder cinemaHallsSeeder) {
        this.filmsSeeder = filmsSeeder;
        this.cinemaHallsSeeder = cinemaHallsSeeder;
    }

    @Override
    public void afterPropertiesSet() throws Exception {
        filmsSeeder.seed();
        cinemaHallsSeeder.seed();
    }
}