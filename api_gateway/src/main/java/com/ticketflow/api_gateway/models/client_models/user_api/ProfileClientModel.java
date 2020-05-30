package com.ticketflow.api_gateway.models.client_models.user_api;

import java.time.LocalDate;

public class ProfileClientModel {
    private Long phoneNumber;
    private LocalDate birthday;

    public ProfileClientModel(Long phoneNumber, LocalDate birthday) {
        this.phoneNumber = phoneNumber;
        this.birthday = birthday;
    }

    public Long getPhoneNumber() {
        return phoneNumber;
    }

    public LocalDate getBirthday() {
        return birthday;
    }
}