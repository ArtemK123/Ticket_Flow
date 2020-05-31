package com.ticketflow.movie_service.domain;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface MoviesJpaRepository extends JpaRepository<MovieDatabaseModel, Integer> {
}