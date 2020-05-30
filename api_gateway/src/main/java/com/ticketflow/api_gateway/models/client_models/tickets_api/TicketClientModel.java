package com.ticketflow.api_gateway.models.client_models.tickets_api;

import com.ticketflow.api_gateway.models.client_models.movies_api.ShortMovieModel;

public class TicketClientModel {
    private Integer id;
    private String buyerEmail;
    private Integer row;
    private Integer seat;
    private Integer price;
    private ShortMovieModel movie;

    public TicketClientModel(Integer id, String buyerEmail, Integer row, Integer seat, Integer price, ShortMovieModel movie) {
        this.id = id;
        this.buyerEmail = buyerEmail;
        this.row = row;
        this.seat = seat;
        this.price = price;
        this.movie = movie;
    }

    public Integer getId() {
        return id;
    }

    public String getBuyerEmail() {
        return buyerEmail;
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
}