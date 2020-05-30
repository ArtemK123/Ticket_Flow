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
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

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
        throw new UnsupportedOperationException("UserApiController.register is called");
        // return ResponseEntity.status(201).body(userService.register(user));
    }

    @PostMapping(value = "/profile")
    public ResponseEntity<ProfileResponseData> profile(@RequestBody String token) throws NotFoundException, InvalidTokenException {
        throw new UnsupportedOperationException("UserApiController.profile is called");
    }

    @PostMapping(value = "/logout")
    public ResponseEntity<String> logout(@RequestBody String token) throws NotFoundException {
        return ResponseEntity.accepted().body(userService.logout(token));
    }
}