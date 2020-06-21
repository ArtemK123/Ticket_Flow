package com.ticketflow.identity_service.models.exceptions;

public class NotUniqueEntityException extends Exception {

    private static final long serialVersionUID = 1300381064308070078L;

    public NotUniqueEntityException(String message) {
        super(message);
    }
}