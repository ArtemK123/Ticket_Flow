package com.ticketflow.ticket_service.models;

public class Ticket {
    private Integer id;
    private Integer movieId;
    private String buyerEmail;
    private Integer row;
    private Integer seat;
    private Integer price;

    public Ticket(Integer id, Integer movieId, String buyerEmail, Integer row, Integer seat, Integer price) {
        this.id = id;
        this.movieId = movieId;
        this.buyerEmail = buyerEmail;
        this.row = row;
        this.seat = seat;
        this.price = price;
    }

    public Integer getId() {
        return id;
    }

    public Integer getMovieId() {
        return movieId;
    }

    public void setMovieId(Integer movieId) {
        this.movieId = movieId;
    }

    public String getBuyerEmail() {
        return buyerEmail;
    }

    public void setBuyerEmail(String buyerEmail) {
        this.buyerEmail = buyerEmail;
    }

    public Integer getRow() {
        return row;
    }

    public void setRow(Integer row) {
        this.row = row;
    }

    public Integer getSeat() {
        return seat;
    }

    public void setSeat(Integer seat) {
        this.seat = seat;
    }

    public Integer getPrice() {
        return price;
    }

    public void setPrice(Integer price) {
        this.price = price;
    }
}