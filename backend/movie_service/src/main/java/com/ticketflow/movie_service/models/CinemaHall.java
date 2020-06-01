package com.ticketflow.movie_service.models;

public class CinemaHall {
    private Integer id;
    private String name;
    private String location;
    private Integer seatRows;
    private Integer seatsInRow;

    public CinemaHall(Integer id, String name, String location, Integer seatRows, Integer seatsInRow) {
        this.id = id;
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