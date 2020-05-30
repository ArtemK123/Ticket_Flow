package com.ticketflow.api_gateway.models.client_models.user_api;

import java.time.LocalDate;

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

    public Long getPhoneNumber() {
        return this.profile.getPhoneNumber();
    }

    public LocalDate getBirthday() {
        return this.profile.getBirthday();
    }
}