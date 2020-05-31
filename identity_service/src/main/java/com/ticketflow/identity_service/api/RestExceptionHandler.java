package com.ticketflow.identity_service.api;

import org.springframework.http.ResponseEntity;

import com.ticketflow.identity_service.models.exceptions.*;

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
}