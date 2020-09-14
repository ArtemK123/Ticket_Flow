package com.ticketflow.api_gateway.proxy.profile;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.proxy.common.parsers.ResponseBodyParser;

import org.springframework.stereotype.Component;

import feign.Response;
import feign.codec.ErrorDecoder;

@Component
class ProfileErrorDecoder implements ErrorDecoder {

    private ResponseBodyParser responseBodyParser;

    public ProfileErrorDecoder(ResponseBodyParser responseBodyParser) {
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