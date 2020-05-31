package com.ticketflow.profile_service.domain;

import java.util.Optional;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface ProfileJpaRepository extends JpaRepository<ProfileDatabaseModel, Integer> {
    @Query("FROM ProfileDatabaseModel WHERE userEmail = ?1")
    Optional<ProfileDatabaseModel> findByUserEmail(String userEmail);
}