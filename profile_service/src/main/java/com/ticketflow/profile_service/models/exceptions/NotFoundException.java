package com.ticketflow.profile_service.models.exceptions;

public class NotFoundException extends Exception {

    private static final long serialVersionUID = -8658461478803471063L;

    public NotFoundException(String message) {
        super(message);
    }
}