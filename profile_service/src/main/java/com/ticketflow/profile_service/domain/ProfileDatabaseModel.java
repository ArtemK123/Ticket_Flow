package com.ticketflow.profile_service.domain;

import java.time.LocalDate;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name = "profiles")
public class ProfileDatabaseModel {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
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