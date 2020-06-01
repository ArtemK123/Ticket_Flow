package com.ticketflow.movie_service.domain.films;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface FilmsJpaRepository extends JpaRepository<FilmDatabaseModel, Integer> {
}