package com.ticketflow.api_gateway.proxy.identity.models.exceptions;

public class WrongPasswordException extends Exception {
    
    private static final long serialVersionUID = -7845490517134555364L;

    public WrongPasswordException(String message) {
        super(message);
    }
}