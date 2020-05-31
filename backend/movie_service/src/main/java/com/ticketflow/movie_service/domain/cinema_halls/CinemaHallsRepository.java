package com.ticketflow.movie_service.domain.cinema_halls;

import java.util.List;
import java.util.stream.Collectors;

import com.ticketflow.movie_service.models.CinemaHall;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

@Repository
public class CinemaHallsRepository {
    private CinemaHallsJpaRepository cinemaHallsJpaRepository;
    private CinemaHallConverter cinemaHallConverter;

    @Autowired
    public CinemaHallsRepository(CinemaHallsJpaRepository cinemaHallsJpaRepository, CinemaHallConverter cinemaHallConverter) {
        this.cinemaHallsJpaRepository = cinemaHallsJpaRepository;
        this.cinemaHallConverter = cinemaHallConverter;
    }

    public List<CinemaHall> getAll() {
        return cinemaHallsJpaRepository.findAll().stream().map(cinemaHallConverter::convert).collect(Collectors.toList());
    }

    public Integer add(CinemaHall cinemaHall) {
        CinemaHallDatabaseModel cinemaHallDatabaseModel = cinemaHallConverter.convert(cinemaHall);
        cinemaHallsJpaRepository.saveAndFlush(cinemaHallDatabaseModel);
        return cinemaHallDatabaseModel.getId();
    }
}