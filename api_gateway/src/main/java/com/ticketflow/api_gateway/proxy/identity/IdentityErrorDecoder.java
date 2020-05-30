package com.ticketflow.api_gateway.proxy.identity;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.identity_service.exceptions.NotUniqueEntityException;
import com.ticketflow.api_gateway.models.identity_service.exceptions.WrongPasswordException;
import com.ticketflow.api_gateway.proxy.common.parsers.ResponseBodyParser;

import org.springframework.stereotype.Component;

import feign.Response;
import feign.codec.ErrorDecoder;

@Component
class IdentityErrorDecoder implements ErrorDecoder {

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