package com.ticketflow.identity_service.service;

import org.springframework.stereotype.Component;

@Component
public class JwtGenerator {
    private static final int HOURS_IN_DAY = 24;

    public String generate() {
        return generate(7 * HOURS_IN_DAY);
    }

    public String generate(int expireHours) {
        throw new UnsupportedOperationException();
    }
}