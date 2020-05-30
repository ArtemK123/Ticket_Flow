package com.ticketflow.api_gateway.service.validators;

import com.ticketflow.api_gateway.models.exceptions.InvalidTokenException;

import org.springframework.stereotype.Component;

@Component
public class TokenValidator {
    private static final String EMPTY_TOKEN_EXCEPTION_MESSAGE = "Token is empty";

    public void validate(String token) throws InvalidTokenException {
        if (token == null || token.isEmpty()) {
            throw new InvalidTokenException(String.format(EMPTY_TOKEN_EXCEPTION_MESSAGE));
        }
    }
}