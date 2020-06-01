package com.ticketflow.movie_service.domain.films;

import java.time.LocalDate;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name = "films")
public class FilmDatabaseModel {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer id;
 
    public FilmDatabaseModel() {
    }

    public FilmDatabaseModel(
            String title,
            String description,
            LocalDate premiereDate,
            String creator,
            Integer duration,
            Integer ageLimit) {
        this.title = title;
        this.description = description;
        this.premiereDate = premiereDate;
        this.creator = creator;
        this.duration = duration;
        this.ageLimit = ageLimit;
    }

    private String title;
    private String description;
    private LocalDate premiereDate;
    private String creator;
    private Integer duration;
    private Integer ageLimit;

    public Integer getId() {
        return id;
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