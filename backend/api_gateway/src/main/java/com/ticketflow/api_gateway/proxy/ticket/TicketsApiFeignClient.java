package com.ticketflow.api_gateway.proxy.ticket;

import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;

import java.util.List;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.ticket_service.OrderModel;
import com.ticketflow.api_gateway.models.ticket_service.Ticket;
import com.ticketflow.api_gateway.models.ticket_service.exceptions.TicketAlreadyOrderedException;

@FeignClient(name = "${ticket.service.name}", configuration = TicketFeignConfiguration.class)
interface TicketsApiFeignClient {
    @GetMapping(value = "/")
    public ResponseEntity<String> home();

    @GetMapping(value = "/tickets/by-movie/{movieId}")
    public ResponseEntity<List<Ticket>> getByMovieId(@PathVariable Integer movieId);

    @PostMapping(value = "/tickets/by-user")
    public ResponseEntity<List<Ticket>> getByUserEmail(@RequestBody String userEmail);

    @PostMapping(value = "/tickets")
    public ResponseEntity<Integer> add(Ticket ticket);

    @PostMapping(value = "/tickets/order")
    public ResponseEntity<String> order(@RequestBody OrderModel orderModel)
        throws TicketAlreadyOrderedException, NotFoundException;
}