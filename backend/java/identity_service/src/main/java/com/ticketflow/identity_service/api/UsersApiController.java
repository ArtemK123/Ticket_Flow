package com.ticketflow.identity_service.api;

import com.ticketflow.identity_service.models.User;
import com.ticketflow.identity_service.models.client_models.LoginModel;
import com.ticketflow.identity_service.models.exceptions.NotFoundException;
import com.ticketflow.identity_service.models.exceptions.NotUniqueEntityException;
import com.ticketflow.identity_service.models.exceptions.WrongPasswordException;
import com.ticketflow.identity_service.service.UserService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class UsersApiController {
    private UserService userService;

    @Autowired
    public UsersApiController(UserService userService) {
        this.userService = userService;
    }

    @GetMapping(value = "/")
    public ResponseEntity<String> home() {
        return ResponseEntity.ok().body("<span>Hello from TicketFlow Identity service</span>");
    }

    @PostMapping(value = "/users/getByToken")
    public ResponseEntity<User> getByToken(@RequestBody String token) throws NotFoundException {
        User user = userService.getByToken(token);
        return ResponseEntity.ok().body(user);
    }

    @PostMapping(value = "/users/getByEmail")
    public ResponseEntity<User> getByEmail(@RequestBody String email) throws NotFoundException {
        User user = userService.getByEmail(email);
        return ResponseEntity.ok().body(user);
    }

    @PostMapping(value = "/users/login")
    public ResponseEntity<String> login(@RequestBody LoginModel loginModel) throws NotFoundException, WrongPasswordException {
        String token = userService.login(loginModel);
        return ResponseEntity.accepted().body(token);
    }

    @PostMapping(value = "/users/register")
    public ResponseEntity<String> register(@RequestBody User user) throws NotUniqueEntityException {
        userService.register(user);
        return ResponseEntity.status(201).body(String.format("Registered successfully user with email=%s", user.getEmail()));
    }

    @PostMapping(value = "/users/logout")
    public ResponseEntity<String> logout(@RequestBody String token) throws NotFoundException {
        userService.logout(token);
        return ResponseEntity.accepted().body("Logout successful");
    }
}