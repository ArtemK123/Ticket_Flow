package com.ticketflow.api_gateway.api;

import com.ticketflow.api_gateway.proxy.common.models.NotFoundException;
import com.ticketflow.api_gateway.proxy.identity.feign_client.IdentityFiegnClient;
import com.ticketflow.api_gateway.proxy.identity.models.LoginModel;
import com.ticketflow.api_gateway.proxy.identity.models.Role;
import com.ticketflow.api_gateway.proxy.identity.models.User;
import com.ticketflow.api_gateway.proxy.identity.models.exceptions.NotUniqueEntityException;
import com.ticketflow.api_gateway.proxy.identity.models.exceptions.WrongPasswordException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class AuthApiController {
    private IdentityFiegnClient identityFiegnClient;

    @Autowired
    public AuthApiController(IdentityFiegnClient identityFiegnClient) {
        this.identityFiegnClient = identityFiegnClient;
    }

    @PostMapping(value = "/login")
    public ResponseEntity<String> login(@RequestBody LoginModel loginModel) throws NotFoundException, WrongPasswordException {
        return identityFiegnClient.login(loginModel);
    }

    @PostMapping(value = "/register")
    public ResponseEntity<String> register(@RequestBody User user) throws NotUniqueEntityException {
        user.setRole(Role.USER);
        return identityFiegnClient.register(user);
    }

    @PostMapping(value = "/logout")
    public ResponseEntity<String> logout(@RequestBody String token) throws NotFoundException {
        return identityFiegnClient.logout(token);
    }
}