package com.ticketflow.api_gateway.proxy.profile;

import com.ticketflow.api_gateway.proxy.common.parsers.ResponseBodyParser;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;

import feign.codec.ErrorDecoder;

class ProfileFeignConfiguration {
    
    private ResponseBodyParser responseBodyParser;

    @Autowired
    public ProfileFeignConfiguration(ResponseBodyParser responseBodyParser) {
        this.responseBodyParser = responseBodyParser;
    }

    @Bean
    public ErrorDecoder feignErrorDecoder() {
        return new ProfileErrorDecoder(responseBodyParser);
    }
}