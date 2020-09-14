package com.ticketflow.movie_service.domain.cinema_halls;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface CinemaHallsJpaRepository extends JpaRepository<CinemaHallDatabaseModel, Integer> {
}