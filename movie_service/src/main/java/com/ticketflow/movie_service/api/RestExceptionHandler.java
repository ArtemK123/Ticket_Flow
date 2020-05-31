package com.ticketflow.movie_service.api;

import org.springframework.http.ResponseEntity;

import com.ticketflow.movie_service.models.exceptions.*;

import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.servlet.mvc.method.annotation.ResponseEntityExceptionHandler;

@ControllerAdvice
public class RestExceptionHandler extends ResponseEntityExceptionHandler {
    @ExceptionHandler(NotFoundException.class)
    protected ResponseEntity<String> handleNotFoundException(NotFoundException exception) {
        logger.warn(exception.getMessage());

        return ResponseEntity.notFound().build();
    }
}