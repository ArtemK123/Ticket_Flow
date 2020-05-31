package com.ticketflow.api_gateway.api;

import com.ticketflow.api_gateway.models.exceptions.InvalidTokenException;
import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.identity_service.exceptions.NotUniqueEntityException;
import com.ticketflow.api_gateway.models.identity_service.exceptions.WrongPasswordException;
import com.ticketflow.api_gateway.models.ticket_service.exceptions.TicketAlreadyOrderedException;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.servlet.mvc.method.annotation.ResponseEntityExceptionHandler;


@ControllerAdvice
public class RestExceptionHandler extends ResponseEntityExceptionHandler {
    private static final String NOT_UNIQUE_USER_EXCEPTION_MESSAGE = "User with given email already exists";
    
    @ExceptionHandler(NotUniqueEntityException.class)
    protected ResponseEntity<String> handleNotUniqueEntityException(NotUniqueEntityException exception) {
        String exceptionMessage = exception.getMessage();
        
        logger.warn(exceptionMessage);

        return ResponseEntity.status(400).body(NOT_UNIQUE_USER_EXCEPTION_MESSAGE);
    }

    @ExceptionHandler(WrongPasswordException.class)
    protected ResponseEntity<String> handleWrongPasswordException(WrongPasswordException exception) {
        String exceptionMessage = exception.getMessage();
        
        logger.warn(exceptionMessage);

        return ResponseEntity.status(401).body(exceptionMessage);
    }

    @ExceptionHandler(TicketAlreadyOrderedException.class)
    protected ResponseEntity<String> handleTicketAlreadyOrderedException(TicketAlreadyOrderedException exception) {
        String exceptionMessage = exception.getMessage();
        
        logger.warn(exceptionMessage);

        return ResponseEntity.status(400).body(exceptionMessage);
    }

    @ExceptionHandler(InvalidTokenException.class)
    protected ResponseEntity<String> handleInvalidTokenException(InvalidTokenException exception) {
        String exceptionMessage = exception.getMessage();
        
        logger.warn(exceptionMessage);

        return ResponseEntity.status(401).body(exceptionMessage);
    }

    @ExceptionHandler(NotFoundException.class)
    protected ResponseEntity<String> handleNotFoundException(NotFoundException exception) {
        logger.warn(exception.getMessage());

        return ResponseEntity.status(404).build();
    }

    @ExceptionHandler(UnsupportedOperationException.class)
    protected ResponseEntity<String> handleUnsupportedOperationException(UnsupportedOperationException exception) {
        String exceptionMessage = exception.getMessage();
        
        logger.warn(exceptionMessage);

        return ResponseEntity.status(500).body("Not yet implemented");
    }

    @ExceptionHandler(Exception.class)
    protected ResponseEntity<String> handleException(Exception exception) {
        logger.error("Unhandled exception.", exception);

        return ResponseEntity.status(500).body("Internal server error. Calm down - it isn`t scary. Just contact with our team and we will fix it");
    }
}