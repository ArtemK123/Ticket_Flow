package com.ticketflow.api_gateway.models.exceptions;

public class InvalidTokenException extends Exception{

    private static final long serialVersionUID = -5519462712023819142L;

    public InvalidTokenException(String message) {
        super(message);
    }
}