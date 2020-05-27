package com.ticketflow.profile_service.api;

import org.springframework.http.ResponseEntity;

import com.ticketflow.profile_service.models.exceptions.NotFoundException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.servlet.mvc.method.annotation.ResponseEntityExceptionHandler;

@ControllerAdvice
public class RestExceptionHandler extends ResponseEntityExceptionHandler {
    private Logger logger;

    @Autowired
    public RestExceptionHandler() {
        logger = LoggerFactory.getLogger(RestExceptionHandler.class);
    }

    @ExceptionHandler(NotFoundException.class)
    protected ResponseEntity<String> handleNotFoundException(NotFoundException exception) {
        logger.error(exception.getMessage());

        return ResponseEntity.notFound().build();
    }
}