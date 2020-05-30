package com.ticketflow.api_gateway.proxy.identity.models.exceptions;

public class NotUniqueEntityException extends Exception {

    private static final long serialVersionUID = 1300381064308070078L;

    public NotUniqueEntityException(String message) {
        super(message);
    }
}