package com.ticketflow.api_gateway.models.client_models.tickets_api;

import java.util.Optional;

import com.ticketflow.api_gateway.models.client_models.movies_api.ShortMovieModel;

public class TicketClientModel {
    private Integer id;
    private String buyerEmail;
    private Integer row;
    private Integer seat;
    private Integer price;
    private ShortMovieModel movie;

    public TicketClientModel(Integer id, Optional<String> optionalBuyerEmail, Integer row, Integer seat, Integer price, ShortMovieModel movie) {
        this.id = id;
        this.setBuyerEmail(optionalBuyerEmail);
        this.row = row;
        this.seat = seat;
        this.price = price;
        this.movie = movie;
    }

    public Integer getId() {
        return id;
    }

    public Optional<String> getBuyerEmail() {
        return Optional.ofNullable(buyerEmail);
    }

    public Integer getRow() {
        return row;
    }

    public Integer getSeat() {
        return seat;
    }

    public Integer getPrice() {
        return price;
    }

    public ShortMovieModel getMovie() {
        return movie;
    }

    private void setBuyerEmail(Optional<String> optionalBuyerEmail) {
        if (optionalBuyerEmail.isPresent()) {
            this.buyerEmail = optionalBuyerEmail.get();
        }
        this.buyerEmail = null;
    }
}