package com.ticketflow.api_gateway.models.client_models.tickets_api;

import com.ticketflow.api_gateway.models.client_models.movies_api.ShortMovieDescription;

public class TicketClientModel {
    private Integer id;
    private String buyerEmail;
    private Integer price;
    private ShortMovieDescription movie;

    public TicketClientModel(Integer id, String buyerEmail, Integer price, ShortMovieDescription movie) {
        this.id = id;
        this.buyerEmail = buyerEmail;
        this.price = price;
        this.movie = movie;
    }

    public Integer getId() {
        return id;
    }

    public String getBuyerEmail() {
        return buyerEmail;
    }

    public Integer getPrice() {
        return price;
    }

    public ShortMovieDescription getMovie() {
        return movie;
    }
}