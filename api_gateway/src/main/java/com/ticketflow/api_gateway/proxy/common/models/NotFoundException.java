package com.ticketflow.api_gateway.proxy.common.models;

public class NotFoundException extends Exception {

    private static final long serialVersionUID = 6231819336031680389L;

    public NotFoundException(String message) {
        super(message);
    }
}