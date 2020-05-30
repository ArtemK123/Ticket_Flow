package com.ticketflow.api_gateway.proxy.identity;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.identity_service.LoginModel;
import com.ticketflow.api_gateway.models.identity_service.User;
import com.ticketflow.api_gateway.models.identity_service.exceptions.NotUniqueEntityException;
import com.ticketflow.api_gateway.models.identity_service.exceptions.WrongPasswordException;

import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;

@FeignClient(name = "${identity.service.name}", configuration = IdentityFeignConfiguration.class)
interface IdentityFeignClient {
    @GetMapping(value = "/")
    public ResponseEntity<String> home();

    @PostMapping(value = "/users/getByToken")
    public ResponseEntity<User> getByToken(@RequestBody String token) throws NotFoundException;

    @PostMapping(value = "/users/getByEmail")
    public ResponseEntity<User> getByEmail(@RequestBody String email) throws NotFoundException;

    @PostMapping(value = "/users/login")
    public ResponseEntity<String> login(@RequestBody LoginModel loginModel) throws NotFoundException, WrongPasswordException;

    @PostMapping(value = "/users/register")
    public ResponseEntity<String> register(@RequestBody User user) throws NotUniqueEntityException;

    @PostMapping(value = "/users/logout")
    public ResponseEntity<String> logout(@RequestBody String token) throws NotFoundException;
}