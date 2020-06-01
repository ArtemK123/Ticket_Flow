package com.ticketflow.ticket_service.domain;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
interface TicketsJpaRepository extends JpaRepository<TicketDatabaseModel, Integer> {
    @Query("FROM TicketDatabaseModel WHERE buyerEmail = ?1")
    List<TicketDatabaseModel> findByBuyerEmail(String email);

    @Query("FROM TicketDatabaseModel WHERE movieId = ?1")
    List<TicketDatabaseModel> findByMovieId(Integer id);
}