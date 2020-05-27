package com.ticketflow.profile_service.models;

import java.time.LocalDate;

public class Profile {
    private Integer id;
    private String userEmail;
    private Long phoneNumber;
    private LocalDate birthday;

    public Profile(Integer id, String userEmail, Long phoneNumber, LocalDate birthday) {
        this.id = id;
        this.userEmail = userEmail;
        this.phoneNumber = phoneNumber;
        this.birthday = birthday;
    }

    public Integer getId() {
        return id;
    }

    public String getUserEmail() {
        return userEmail;
    }

    public void setUserEmail(String userEmail) {
        this.userEmail = userEmail;
    }

    public Long getPhoneNumber() {
        return phoneNumber;
    }

    public void setPhoneNumber(Long phoneNumber) {
        this.phoneNumber = phoneNumber;
    }

    public LocalDate getBirthday() {
        return birthday;
    }

    public void setBirthday(LocalDate birthday) {
        this.birthday = birthday;
    }
}