package com.ticketflow.api_gateway.api;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class HomeApiController {
    @GetMapping(value = "/")
    public ResponseEntity<String> getAll() {
        return ResponseEntity.ok("Hello from TicketFlow backend");
    }

}