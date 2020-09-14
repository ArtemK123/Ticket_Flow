package com.ticketflow.api_gateway.proxy.common.parsers;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.nio.charset.StandardCharsets;
import java.util.stream.Collectors;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Component;

import feign.Response;

@Component
public class ResponseBodyParser {

    private Logger logger;

    public ResponseBodyParser() {
        logger = LoggerFactory.getLogger(this.getClass());
    }

    public String parseResponseBody(Response response) {
        try (InputStream inputStream = response.body().asInputStream()) {
            return new BufferedReader(new InputStreamReader(inputStream, StandardCharsets.UTF_8))
                .lines()
                .collect(Collectors.joining("\n"));
        } catch (IOException exception) {
            logger.error("Failed to read reponse body", exception);
            return "";
        }
    }
}