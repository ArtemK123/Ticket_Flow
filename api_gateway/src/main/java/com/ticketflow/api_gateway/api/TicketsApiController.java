package com.ticketflow.api_gateway.api;

import java.util.List;

import com.ticketflow.api_gateway.models.client_models.tickets_api.OrderRequestModel;
import com.ticketflow.api_gateway.models.client_models.tickets_api.TicketClientModel;
import com.ticketflow.api_gateway.models.exceptions.InvalidTokenException;
import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.ticket_service.exceptions.TicketAlreadyOrderedException;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestParam;

public class TicketsApiController {
    @GetMapping(value = "tickets/by-movie/{id}")
    public ResponseEntity<List<TicketClientModel>> ticketsByMovie(@RequestParam Integer id) throws NotFoundException {
        throw new UnsupportedOperationException("TicketsApiController.ticketsByMovie is called");
    }

    @PostMapping(value = "/tickets/by-user")
    public ResponseEntity<List<TicketClientModel>> ticketsByUser(@RequestBody String token) throws InvalidTokenException {
        throw new UnsupportedOperationException("TicketsApiController.ticketsByUser is called");
    }

    @PostMapping(value = "/tickets/order")
    public ResponseEntity<String> order(@RequestBody OrderRequestModel orderRequestModel)
            throws TicketAlreadyOrderedException,
                InvalidTokenException,
                NotFoundException {
        throw new UnsupportedOperationException("TicketsApiController.order is called");
    }
}