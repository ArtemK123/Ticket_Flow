package com.ticketflow.identity_service.models;

import java.util.Optional;

public class User {
    private String email;
    private String password;
    private String token;
    private Role role;

    public User(String email, String password, Optional<String> optionalToken, Role role) {
        this.email = email;
        this.password = password;
        this.setToken(optionalToken);
        this.role = role;
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

    public void setToken(Optional<String> optionalToken) {
        if (optionalToken.isPresent()) {
            this.token = optionalToken.get();
        }
        else {
            this.token = null;
        }
    }

    public Role getRole() {
        return role;
    }

    public void setRole(Role role) {
        this.role = role;
    }
}