package com.ticketflow.ticket_service.domain;

import java.util.Optional;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name = "tickets")
public class TicketDatabaseModel {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer id;

    private Integer movieId;
    private String buyerEmail;
    private Integer row;
    private Integer seat;
    private Integer price;

    public TicketDatabaseModel(Integer movieId, Optional<String> optionalBuyerEmail, Integer row, Integer seat, Integer price) {
        this.movieId = movieId;
        this.setBuyerEmail(optionalBuyerEmail);
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

    public Optional<String> getBuyerEmail() {
        return Optional.ofNullable(buyerEmail);
    }

    public void setBuyerEmail(Optional<String> optionalBuyerEmail) {
        if (optionalBuyerEmail.isPresent()) {
            this.buyerEmail = optionalBuyerEmail.get();
        }
        this.buyerEmail = null;
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