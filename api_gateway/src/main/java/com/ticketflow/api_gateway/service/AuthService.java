package com.ticketflow.api_gateway.service;

import com.ticketflow.api_gateway.proxy.common.models.NotFoundException;
import com.ticketflow.api_gateway.proxy.identity.models.LoginModel;
import com.ticketflow.api_gateway.proxy.identity.models.Role;
import com.ticketflow.api_gateway.proxy.identity.models.User;
import com.ticketflow.api_gateway.proxy.identity.models.exceptions.NotUniqueEntityException;
import com.ticketflow.api_gateway.proxy.identity.models.exceptions.WrongPasswordException;
import com.ticketflow.api_gateway.proxy.identity.service_proxy.IdentityServiceProxy;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class AuthService {
    private IdentityServiceProxy identityServiceProxy;

    @Autowired
    public AuthService(IdentityServiceProxy identityServiceProxy) {
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