package com.ticketflow.api_gateway.api;

import java.util.List;

import com.ticketflow.api_gateway.models.client_models.tickets_api.OrderRequestModel;
import com.ticketflow.api_gateway.models.client_models.tickets_api.TicketClientModel;
import com.ticketflow.api_gateway.models.exceptions.InvalidTokenException;
import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.ticket_service.exceptions.TicketAlreadyOrderedException;
import com.ticketflow.api_gateway.service.TicketsService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class TicketsApiController {
    private TicketsService ticketsService;

    @Autowired
    public TicketsApiController(TicketsService ticketsService) {
        this.ticketsService = ticketsService;
    }

    @GetMapping(value = "tickets/by-movie/{id}")
    public ResponseEntity<List<TicketClientModel>> getTicketsByMovie(@RequestParam Integer id) throws NotFoundException {
        return ResponseEntity.ok(ticketsService.getTicketsByMovie(id));
    }

    @PostMapping(value = "/tickets/by-user")
    public ResponseEntity<List<TicketClientModel>> ticketsByUser(@RequestBody String token) throws InvalidTokenException, NotFoundException {
        return ResponseEntity.ok(ticketsService.getTicketsByUser(token));
    }

    @PostMapping(value = "/tickets/order")
    public ResponseEntity<String> order(@RequestBody OrderRequestModel orderRequestModel)
            throws TicketAlreadyOrderedException,
                InvalidTokenException,
                NotFoundException {

        ticketsService.order(orderRequestModel);
        return ResponseEntity.accepted().body("Ordered successfully");
    }
}