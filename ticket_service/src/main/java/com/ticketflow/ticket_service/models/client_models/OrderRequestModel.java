package com.ticketflow.ticket_service.models.client_models;

public class OrderRequestModel {
    private Integer ticketId;
    private String buyerEmail;

    public OrderRequestModel(Integer ticketId, String buyerEmail) {
        this.ticketId = ticketId;
        this.buyerEmail = buyerEmail;
    }

    public Integer getTicketId() {
        return ticketId;
    }

    public String getBuyerEmail() {
        return buyerEmail;
    }
}