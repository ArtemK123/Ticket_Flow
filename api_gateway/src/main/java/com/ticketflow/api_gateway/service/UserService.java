package com.ticketflow.api_gateway.service;

import java.util.Optional;

import com.ticketflow.api_gateway.models.client_models.user_api.ProfileResponseData;
import com.ticketflow.api_gateway.models.client_models.user_api.RegisterRequestData;
import com.ticketflow.api_gateway.models.exceptions.InvalidTokenException;
import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.identity_service.LoginModel;
import com.ticketflow.api_gateway.models.identity_service.Role;
import com.ticketflow.api_gateway.models.identity_service.User;
import com.ticketflow.api_gateway.models.identity_service.exceptions.NotUniqueEntityException;
import com.ticketflow.api_gateway.models.identity_service.exceptions.WrongPasswordException;
import com.ticketflow.api_gateway.models.profile_service.Profile;
import com.ticketflow.api_gateway.proxy.identity.IdentityServiceProxy;
import com.ticketflow.api_gateway.proxy.profile.ProfileServiceProxy;
import com.ticketflow.api_gateway.service.validators.TokenValidator;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class UserService {
    private IdentityServiceProxy identityServiceProxy;
    private ProfileServiceProxy profileServiceProxy;
    private TokenValidator tokenValidator;

    @Autowired
    public UserService(IdentityServiceProxy identityServiceProxy, ProfileServiceProxy profileServiceProxy, TokenValidator tokenValidator) {
        this.identityServiceProxy = identityServiceProxy;
        this.profileServiceProxy = profileServiceProxy;
        this.tokenValidator = tokenValidator;
    }

    public ProfileResponseData getProfile(String token) throws NotFoundException, InvalidTokenException {
        tokenValidator.validate(token);

        User user = identityServiceProxy.getByToken(token);
        Profile profile = profileServiceProxy.getByUserEmail(user.getEmail());
        return new ProfileResponseData(profile);
    }

    public String login(LoginModel loginModel) throws NotFoundException, WrongPasswordException {
        return identityServiceProxy.login(loginModel);
    }

    public void register(RegisterRequestData registerRequestData) throws NotUniqueEntityException {
        User newUser = new User(
            registerRequestData.getUserEmail(),
            registerRequestData.getPassword(),
            Optional.empty(),
            Role.USER);
        
        identityServiceProxy.register(newUser);
        
        Profile newProfile = new Profile(
            registerRequestData.getUserEmail(),
            registerRequestData.getProfile().getPhoneNumber(),
            registerRequestData.getProfile().getBirthday());

        profileServiceProxy.add(newProfile);
    }

    public String logout(String token) throws NotFoundException {
        return identityServiceProxy.logout(token);
    }
}