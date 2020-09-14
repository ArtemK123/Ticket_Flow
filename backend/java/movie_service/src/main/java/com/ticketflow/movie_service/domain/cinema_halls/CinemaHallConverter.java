package com.ticketflow.movie_service.domain.cinema_halls;

import com.ticketflow.movie_service.models.CinemaHall;

import org.springframework.stereotype.Component;

@Component
public class CinemaHallConverter {
    public CinemaHall convert(CinemaHallDatabaseModel cinemaHallDatabaseModel) {
        return new CinemaHall(
            cinemaHallDatabaseModel.getId(),
            cinemaHallDatabaseModel.getName(),
            cinemaHallDatabaseModel.getLocation(),
            cinemaHallDatabaseModel.getSeatRows(),
            cinemaHallDatabaseModel.getSeatsInRow()
        );
    }

    public CinemaHallDatabaseModel convert(CinemaHall cinemaHall) {
        return new CinemaHallDatabaseModel(
            cinemaHall.getName(),
            cinemaHall.getLocation(),
            cinemaHall.getSeatRows(),
            cinemaHall.getSeatsInRow()
        );
    }
}