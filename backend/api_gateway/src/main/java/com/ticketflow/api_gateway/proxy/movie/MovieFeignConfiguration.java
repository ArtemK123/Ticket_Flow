package com.ticketflow.api_gateway.proxy.movie;

import com.ticketflow.api_gateway.proxy.common.parsers.ResponseBodyParser;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;

import feign.codec.ErrorDecoder;

public class MovieFeignConfiguration {
    
    private ResponseBodyParser responseBodyParser;

    @Autowired
    public MovieFeignConfiguration(ResponseBodyParser responseBodyParser) {
        this.responseBodyParser = responseBodyParser;
    }

    @Bean
    public ErrorDecoder feignErrorDecoder() {
        return new MovieErrorDecoder(responseBodyParser);
    }
}