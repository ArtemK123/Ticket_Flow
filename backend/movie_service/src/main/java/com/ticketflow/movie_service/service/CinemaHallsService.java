package com.ticketflow.movie_service.service;

import java.util.List;

import com.ticketflow.movie_service.domain.cinema_halls.CinemaHallsRepository;
import com.ticketflow.movie_service.models.CinemaHall;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class CinemaHallsService {
    private CinemaHallsRepository cinemaHallsRepository;

    @Autowired
    public CinemaHallsService(CinemaHallsRepository cinemaHallsRepository) {
        this.cinemaHallsRepository = cinemaHallsRepository;
    }

    public List<CinemaHall> getAll() {
        return cinemaHallsRepository.getAll();
    }
    
    public Integer add(CinemaHall cinemaHall) {
        return cinemaHallsRepository.add(cinemaHall);
    }
}