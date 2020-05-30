package com.ticketflow.api_gateway.proxy.identity.feign_client;

import com.ticketflow.api_gateway.proxy.common.models.NotFoundException;
import com.ticketflow.api_gateway.proxy.common.parsers.ResponseBodyParser;
import com.ticketflow.api_gateway.proxy.identity.models.exceptions.NotUniqueEntityException;
import com.ticketflow.api_gateway.proxy.identity.models.exceptions.WrongPasswordException;

import org.springframework.stereotype.Component;

import feign.Response;
import feign.codec.ErrorDecoder;

@Component
public class IdentityErrorDecoder implements ErrorDecoder {

    private ResponseBodyParser responseBodyParser;

    public IdentityErrorDecoder(ResponseBodyParser responseBodyParser) {
        this.responseBodyParser = responseBodyParser;
    }

    @Override
    public Exception decode(String methodKey, Response response) {
        switch (response.status()) {
            case 400: return new NotUniqueEntityException(responseBodyParser.parseResponseBody(response));
            
            case 401: return new WrongPasswordException(responseBodyParser.parseResponseBody(response));

            case 404: return new NotFoundException(responseBodyParser.parseResponseBody(response));
            
            default: return new Exception(response.reason());
        }
    }
}