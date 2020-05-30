package com.ticketflow.api_gateway.api;

import com.ticketflow.api_gateway.proxy.common.models.NotFoundException;
import com.ticketflow.api_gateway.proxy.identity.models.exceptions.NotUniqueEntityException;
import com.ticketflow.api_gateway.proxy.identity.models.exceptions.WrongPasswordException;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.servlet.mvc.method.annotation.ResponseEntityExceptionHandler;


@ControllerAdvice
public class RestExceptionHandler extends ResponseEntityExceptionHandler {
    @ExceptionHandler(NotUniqueEntityException.class)
    protected ResponseEntity<String> handleNotUniqueEntityException(NotUniqueEntityException exception) {
        String exceptionMessage = exception.getMessage();
        
        logger.warn(exceptionMessage);

        return ResponseEntity.status(400).body(exceptionMessage);
    }

    @ExceptionHandler(WrongPasswordException.class)
    protected ResponseEntity<String> handleWrongPasswordException(WrongPasswordException exception) {
        String exceptionMessage = exception.getMessage();
        
        logger.warn(exceptionMessage);

        return ResponseEntity.status(401).body(exceptionMessage);
    }

    @ExceptionHandler(NotFoundException.class)
    protected ResponseEntity<String> handleNotFoundException(NotFoundException exception) {
        String exceptionMessage = exception.getMessage();
        
        logger.warn(exceptionMessage);

        return ResponseEntity.status(404).body(exceptionMessage);
    }

    @ExceptionHandler(Exception.class)
    protected ResponseEntity<String> handleException(Exception exception) {
        logger.error("Unhandled exception.", exception);

        return ResponseEntity.status(500).body("Internal server error. Calm down and wait a little - our team is already working on it");
    }
}