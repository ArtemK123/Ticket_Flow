package com.ticketflow.identity_service.domain;

import java.util.Optional;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

import com.ticketflow.identity_service.models.Role;

@Entity
@Table(name = "users")
public class UserDatabaseModel {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer id;

    @Column(unique=true)
    private String email;
    private String password;
    private String token;
    private Role role;

    public UserDatabaseModel(String email, String password, Role role) {
        this.email = email;
        this.password = password;
        this.role = role;
    }

    public Integer getId() {
        return id;
    }

    public String getEmail() {
        return email;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public Optional<String> getToken() {
        return Optional.ofNullable(token);
    }

    public void setToken(Optional<String> token) {
        if (token.isPresent()) {
            this.token = token.get();
        }
        
        this.token = null;
    }

    public Role getRole() {
        return role;
    }

    public void setRole(Role role) {
        this.role = role;
    }
}