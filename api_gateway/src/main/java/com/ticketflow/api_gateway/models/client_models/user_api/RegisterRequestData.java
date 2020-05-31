package com.ticketflow.api_gateway.models.client_models.user_api;

public class RegisterRequestData {
    private String email;
    private String password;
    private ProfileClientModel profile;

    public String getEmail() {
        return email;
    }

    public String getPassword() {
        return password;
    }

    public ProfileClientModel getProfile() {
        return profile;
    }
}