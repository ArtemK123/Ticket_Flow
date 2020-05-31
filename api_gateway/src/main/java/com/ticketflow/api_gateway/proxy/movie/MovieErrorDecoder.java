package com.ticketflow.api_gateway.proxy.movie;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.proxy.common.parsers.ResponseBodyParser;

import org.springframework.stereotype.Component;

import feign.Response;
import feign.codec.ErrorDecoder;

@Component
class MovieErrorDecoder implements ErrorDecoder {

    private ResponseBodyParser responseBodyParser;

    public MovieErrorDecoder(ResponseBodyParser responseBodyParser) {
        this.responseBodyParser = responseBodyParser;
    }

    @Override
    public Exception decode(String methodKey, Response response) {
        if (response.status() == 404) {
            return new NotFoundException(responseBodyParser.parseResponseBody(response));
        }
            
        return new Exception(response.reason());
    }
}