package com.ticketflow.movie_service.domain.cinema_halls;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name = "cinema_halls")
public class CinemaHallDatabaseModel {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer id;
    private String name;
    private String location;
    private Integer seatRows;
    private Integer seatsInRow;

    public CinemaHallDatabaseModel() {
    }

    public CinemaHallDatabaseModel(String name, String location, Integer seatRows, Integer seatsInRow) {
        this.name = name;
        this.location = location;
        this.seatRows = seatRows;
        this.seatsInRow = seatsInRow;
    }

    public Integer getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }

    public Integer getSeatRows() {
        return seatRows;
    }

    public void setSeatRows(Integer seatRows) {
        this.seatRows = seatRows;
    }

    public Integer getSeatsInRow() {
        return seatsInRow;
    }

    public void setSeatsInRow(Integer seatsInRow) {
        this.seatsInRow = seatsInRow;
    }
}