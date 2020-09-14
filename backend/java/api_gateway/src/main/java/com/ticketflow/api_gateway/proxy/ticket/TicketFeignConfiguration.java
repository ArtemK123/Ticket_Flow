package com.ticketflow.api_gateway.proxy.ticket;

import com.ticketflow.api_gateway.proxy.common.parsers.ResponseBodyParser;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;

import feign.codec.ErrorDecoder;

class TicketFeignConfiguration {
    
    private ResponseBodyParser responseBodyParser;

    @Autowired
    public TicketFeignConfiguration(ResponseBodyParser responseBodyParser) {
        this.responseBodyParser = responseBodyParser;
    }

    @Bean
    public ErrorDecoder feignErrorDecoder() {
        return new TicketErrorDecoder(responseBodyParser);
    }
}