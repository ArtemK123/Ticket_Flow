package com.ticketflow.ticket_service.models.client_models;

public class OrderModel {
    private Integer ticketId;
    private String buyerEmail;

    public OrderModel(Integer ticketId, String buyerEmail) {
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