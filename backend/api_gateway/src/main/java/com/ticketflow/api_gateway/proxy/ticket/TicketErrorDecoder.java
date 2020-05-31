package com.ticketflow.api_gateway.proxy.ticket;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.ticket_service.exceptions.TicketAlreadyOrderedException;
import com.ticketflow.api_gateway.proxy.common.parsers.ResponseBodyParser;

import org.springframework.stereotype.Component;

import feign.Response;
import feign.codec.ErrorDecoder;

@Component
class TicketErrorDecoder implements ErrorDecoder {

    private ResponseBodyParser responseBodyParser;

    public TicketErrorDecoder(ResponseBodyParser responseBodyParser) {
        this.responseBodyParser = responseBodyParser;
    }

    @Override
    public Exception decode(String methodKey, Response response) {
        switch (response.status()) {
            case 400: return new TicketAlreadyOrderedException(responseBodyParser.parseResponseBody(response));

            case 404: return new NotFoundException(responseBodyParser.parseResponseBody(response));
            
            default: return new Exception(response.reason());
        }
    }
}