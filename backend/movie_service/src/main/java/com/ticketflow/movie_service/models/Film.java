package com.ticketflow.movie_service.models;

import java.time.LocalDate;

public class Film {
    private Integer id;
    private String title;
    private String description;
    private LocalDate premiereDate;
    private String creator;
    private Integer duration;
    private Integer ageLimit;

    public Film(
            Integer id,
            String title,
            String description,
            LocalDate premiereDate,
            String creator,
            Integer duration,
            Integer ageLimit) {
        this.id = id;
        this.title = title;
        this.description = description;
        this.premiereDate = premiereDate;
        this.creator = creator;
        this.duration = duration;
        this.ageLimit = ageLimit;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public LocalDate getPremiereDate() {
        return premiereDate;
    }

    public void setPremiereDate(LocalDate premiereDate) {
        this.premiereDate = premiereDate;
    }

    public String getCreator() {
        return creator;
    }

    public void setCreator(String creator) {
        this.creator = creator;
    }

    public Integer getDuration() {
        return duration;
    }

    public void setDuration(Integer duration) {
        this.duration = duration;
    }

    public Integer getAgeLimit() {
        return ageLimit;
    }

    public void setAgeLimit(Integer ageLimit) {
        this.ageLimit = ageLimit;
    }
}