package com.ticketflow.api_gateway.api;

import com.ticketflow.api_gateway.models.client_models.user_api.ProfileResponseData;
import com.ticketflow.api_gateway.models.client_models.user_api.RegisterRequestData;
import com.ticketflow.api_gateway.models.exceptions.InvalidTokenException;
import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.identity_service.LoginModel;
import com.ticketflow.api_gateway.models.identity_service.exceptions.NotUniqueEntityException;
import com.ticketflow.api_gateway.models.identity_service.exceptions.WrongPasswordException;
import com.ticketflow.api_gateway.service.UserService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

@CrossOrigin(origins = "http://localhost:3000")
@RestController
public class UserApiController {
    private UserService userService;

    @Autowired
    public UserApiController(UserService userService) {
        this.userService = userService;
    }

    @PostMapping(value = "/login")
    public ResponseEntity<String> login(@RequestBody LoginModel loginModel) throws NotFoundException, WrongPasswordException {
        return ResponseEntity.accepted().body(userService.login(loginModel));
    }

    @PostMapping(value = "/register")
    public ResponseEntity<String> register(@RequestBody RegisterRequestData registerRequestData) throws NotUniqueEntityException {
        userService.register(registerRequestData);
        return ResponseEntity.status(201).body("Registered successfully");
    }

    @PostMapping(value = "/profile")
    public ResponseEntity<ProfileResponseData> getProfile(@RequestBody String token) throws NotFoundException, InvalidTokenException {
        return ResponseEntity.ok().body(userService.getProfile(token));
    }

    @PostMapping(value = "/logout")
    public ResponseEntity<String> logout(@RequestBody String token) throws NotFoundException {
        return ResponseEntity.accepted().body(userService.logout(token));
    }
}