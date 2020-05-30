package com.ticketflow.ticket_service.api;

import com.ticketflow.ticket_service.service.TicketsService;

import java.util.List;

import com.ticketflow.ticket_service.models.Ticket;
import com.ticketflow.ticket_service.models.client_models.OrderRequestModel;
import com.ticketflow.ticket_service.models.exceptions.NotFoundException;
import com.ticketflow.ticket_service.models.exceptions.TicketAlreadyOrderedException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class TicketsApiController {

    private TicketsService ticketsService;

    @Autowired
    public TicketsApiController(TicketsService ticketsService) {
        this.ticketsService = ticketsService;
    }

    @GetMapping(value = "/")
    public ResponseEntity<String> home() {
        return ResponseEntity.ok().body("<span>Hello from TicketFlow Ticket service</span>");
    }

    @GetMapping(value = "tickets/by-movie/{id}")
    public ResponseEntity<List<Ticket>> getByMovie(@PathVariable Integer movieId) {
        return ResponseEntity.ok(ticketsService.getByMovieId(movieId));
    }

    @PostMapping(value = "/tickets/by-user")
    public ResponseEntity<List<Ticket>> getByUser(@RequestBody String buyerEmail) {
        return ResponseEntity.ok(ticketsService.getByBuyerEmail(buyerEmail));
    }

    @PostMapping(value = "/tickets/order")
    public ResponseEntity<String> order(@RequestBody OrderRequestModel orderRequestModel)
            throws TicketAlreadyOrderedException, NotFoundException {
        ticketsService.order(orderRequestModel);
        return ResponseEntity.accepted().body("Ordered successfully");
    }
}