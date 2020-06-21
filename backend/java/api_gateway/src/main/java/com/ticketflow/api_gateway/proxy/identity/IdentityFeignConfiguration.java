package com.ticketflow.api_gateway.proxy.identity;

import com.ticketflow.api_gateway.proxy.common.parsers.ResponseBodyParser;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;

import feign.codec.ErrorDecoder;

class IdentityFeignConfiguration {
    
    private ResponseBodyParser responseBodyParser;

    @Autowired
    public IdentityFeignConfiguration(ResponseBodyParser responseBodyParser) {
        this.responseBodyParser = responseBodyParser;
    }

    @Bean
    public ErrorDecoder feignErrorDecoder() {
        return new IdentityErrorDecoder(responseBodyParser);
    }
}