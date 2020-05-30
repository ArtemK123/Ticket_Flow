package com.ticketflow.api_gateway.models.client_models.user_api;

public class RegisterRequestData {
    private String userEmail;
    private String password;
    private ProfileClientModel profile;

    public String getUserEmail() {
        return userEmail;
    }

    public String getPassword() {
        return password;
    }

    public ProfileClientModel getProfile() {
        return profile;
    }
}