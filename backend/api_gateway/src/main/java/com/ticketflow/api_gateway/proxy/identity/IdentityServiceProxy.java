package com.ticketflow.api_gateway.proxy.identity;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.identity_service.LoginModel;
import com.ticketflow.api_gateway.models.identity_service.User;
import com.ticketflow.api_gateway.models.identity_service.exceptions.NotUniqueEntityException;
import com.ticketflow.api_gateway.models.identity_service.exceptions.WrongPasswordException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class IdentityServiceProxy {
    private IdentityFeignClient identityFeignClient;

    @Autowired
    public IdentityServiceProxy(IdentityFeignClient identityFeignClient) {
        this.identityFeignClient = identityFeignClient;
    }

    public User getByToken(String token) throws NotFoundException {
        return identityFeignClient.getByToken(token).getBody();
    }

    public User getByEmail(String email) throws NotFoundException {
        return identityFeignClient.getByEmail(email).getBody();
    }

    public String login(LoginModel loginModel) throws NotFoundException, WrongPasswordException {
        return identityFeignClient.login(loginModel).getBody();
    }

    public String register(User user) throws NotUniqueEntityException {
        return identityFeignClient.register(user).getBody();
    }

    public String logout(String token) throws NotFoundException {
        return identityFeignClient.logout(token).getBody();
    }
}