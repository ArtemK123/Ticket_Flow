package com.ticketflow.ticket_service.models.exceptions;

public class TicketAlreadyOrderedException extends Exception {

    private static final long serialVersionUID = 8734892439982686793L;

    public TicketAlreadyOrderedException(String message) {
        super(message);
    }
}