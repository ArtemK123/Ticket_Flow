package com.ticketflow.identity_service.domain;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface UserJpaRepository extends JpaRepository<UserDatabaseModel, Integer> {
    @Query("FROM UserDatabaseModel WHERE email = ?1")
    UserDatabaseModel findByEmail(String email);

    @Query("FROM UserDatabaseModel WHERE token = ?1")
    UserDatabaseModel findByToken(String token);
}