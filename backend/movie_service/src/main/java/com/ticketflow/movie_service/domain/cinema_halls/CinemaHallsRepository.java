package com.ticketflow.movie_service.domain.cinema_halls;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

import com.ticketflow.movie_service.models.CinemaHall;
import com.ticketflow.movie_service.models.exceptions.NotFoundException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

@Repository
public class CinemaHallsRepository {
    private static final String NOT_FOUND_BY_ID_EXCEPTION_MESSAGE = "Cinema hall with id=%d is not found";

    private CinemaHallsJpaRepository cinemaHallsJpaRepository;
    private CinemaHallConverter cinemaHallConverter;

    @Autowired
    public CinemaHallsRepository(CinemaHallsJpaRepository cinemaHallsJpaRepository, CinemaHallConverter cinemaHallConverter) {
        this.cinemaHallsJpaRepository = cinemaHallsJpaRepository;
        this.cinemaHallConverter = cinemaHallConverter;
    }

    public CinemaHall getById(Integer id) throws NotFoundException {
        Optional<CinemaHallDatabaseModel> optionalCinemaHallDatabaseModel = cinemaHallsJpaRepository.findById(id);

        if (optionalCinemaHallDatabaseModel.isEmpty()) {
            throw new NotFoundException(String.format(NOT_FOUND_BY_ID_EXCEPTION_MESSAGE, id));            
        }

        return cinemaHallConverter.convert(optionalCinemaHallDatabaseModel.get());
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