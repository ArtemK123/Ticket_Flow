package com.ticketflow.api_gateway.models.client_models.tickets_api;

public class OrderRequestModel {
    private Integer ticketId; 
    private String token;

    public Integer getTicketId() {
        return ticketId;
    }

    public String getToken() {
        return token;
    }
}