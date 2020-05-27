package com.ticketflow.profile_service.models;

import java.time.LocalDate;

public class Profile {
    private Integer id;
    private String userEmail;
    private Long phoneNumber;
    private LocalDate birthday;

	public Integer getId() {
        return id;
    }

    public String getUserEmail() {
		return userEmail;
    }
    
    public Long getPhoneNumber() {
        return phoneNumber;
    }

    public LocalDate getBirthday() {
        return birthday;
    }

    public void setId(Integer id) {
        this.id = id;
    }

	public void setUserEmail(String userEmail) {
		this.userEmail = userEmail;
	}

    public void setPhoneNumber(Long phoneNumber) {
        this.phoneNumber = phoneNumber;
    }

    public void setBirthday(LocalDate birthday) {
        this.birthday = birthday;
    }
}