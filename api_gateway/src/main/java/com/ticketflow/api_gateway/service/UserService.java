package com.ticketflow.api_gateway.service;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.identity_service.LoginModel;
import com.ticketflow.api_gateway.models.identity_service.Role;
import com.ticketflow.api_gateway.models.identity_service.User;
import com.ticketflow.api_gateway.models.identity_service.exceptions.NotUniqueEntityException;
import com.ticketflow.api_gateway.models.identity_service.exceptions.WrongPasswordException;
import com.ticketflow.api_gateway.proxy.identity.IdentityServiceProxy;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class UserService {
    private IdentityServiceProxy identityServiceProxy;

    @Autowired
    public UserService(IdentityServiceProxy identityServiceProxy) {
        this.identityServiceProxy = identityServiceProxy;
    }

    public String login(LoginModel loginModel) throws NotFoundException, WrongPasswordException {
        return identityServiceProxy.login(loginModel);
    }

    public String register(User user) throws NotUniqueEntityException {
        user.setRole(Role.USER);
        return identityServiceProxy.register(user);
    }

    public String logout(String token) throws NotFoundException {
        return identityServiceProxy.logout(token);
    }
}