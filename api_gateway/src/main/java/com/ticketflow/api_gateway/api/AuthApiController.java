package com.ticketflow.api_gateway.api;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.identity.LoginModel;
import com.ticketflow.api_gateway.models.identity.Role;
import com.ticketflow.api_gateway.models.identity.User;
import com.ticketflow.api_gateway.models.identity.exceptions.NotUniqueEntityException;
import com.ticketflow.api_gateway.models.identity.exceptions.WrongPasswordException;
import com.ticketflow.api_gateway.service.AuthService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class AuthApiController {
    private AuthService authService;

    @Autowired
    public AuthApiController(AuthService authService) {
        this.authService = authService;
    }

    @PostMapping(value = "/login")
    public ResponseEntity<String> login(@RequestBody LoginModel loginModel) throws NotFoundException, WrongPasswordException {
        return ResponseEntity.accepted().body(authService.login(loginModel));
    }

    @PostMapping(value = "/register")
    public ResponseEntity<String> register(@RequestBody User user) throws NotUniqueEntityException {
        user.setRole(Role.USER);
        return ResponseEntity.status(201).body(authService.register(user));
    }

    @PostMapping(value = "/logout")
    public ResponseEntity<String> logout(@RequestBody String token) throws NotFoundException {
        return ResponseEntity.accepted().body(authService.logout(token));
    }
}