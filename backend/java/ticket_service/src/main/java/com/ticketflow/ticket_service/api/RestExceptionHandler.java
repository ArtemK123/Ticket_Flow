package com.ticketflow.ticket_service.api;

import org.springframework.http.ResponseEntity;

import com.ticketflow.ticket_service.models.exceptions.*;

import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.servlet.mvc.method.annotation.ResponseEntityExceptionHandler;

@ControllerAdvice
public class RestExceptionHandler extends ResponseEntityExceptionHandler {
    @ExceptionHandler(NotFoundException.class)
    protected ResponseEntity<String> handleNotFoundException(NotFoundException exception) {
        String exceptionMessage = exception.getMessage();
        
        logger.warn(exceptionMessage);

        return ResponseEntity.status(404).body(exceptionMessage);
    }

    @ExceptionHandler(TicketAlreadyOrderedException.class)
    protected ResponseEntity<String> handleTicketAlreadyOrderedException(TicketAlreadyOrderedException exception) {
        String exceptionMessage = exception.getMessage();
        
        logger.warn(exceptionMessage);

        return ResponseEntity.status(400).body(exceptionMessage);
    }
}