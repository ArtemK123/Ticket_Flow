package com.ticketflow.identity_service.service;

import java.util.Optional;

import com.ticketflow.identity_service.api.client_models.LoginModel;
import com.ticketflow.identity_service.domain.UserRepository;
import com.ticketflow.identity_service.models.User;
import com.ticketflow.identity_service.models.exceptions.NotFoundException;
import com.ticketflow.identity_service.models.exceptions.NotUniqueEntityException;
import com.ticketflow.identity_service.models.exceptions.WrongPasswordException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class UserService {
    private static final String WRONG_PASSWORD_EXCEPTION_MESSAGE = "Wrong password for user with email=%s";
    
    private UserRepository userRepository;
    private JwtGenerator jwtGenerator;

    @Autowired
    public UserService(UserRepository userRepository, JwtGenerator jwtGenerator) {
        this.userRepository = userRepository;
        this.jwtGenerator = jwtGenerator;
    }

    public User getByToken(String token) throws NotFoundException {
        return userRepository.getByToken(token);
    }

    public User getByEmail(String email) throws NotFoundException {
        return userRepository.getByEmail(email);
    }

    public String login(LoginModel loginModel) throws NotFoundException, WrongPasswordException {
        User user = userRepository.getByEmail(loginModel.getEmail());
        
        if (!user.getPassword().equals(loginModel.getPassword())) {
            throw new WrongPasswordException(String.format(WRONG_PASSWORD_EXCEPTION_MESSAGE, loginModel.getEmail()));
        }

        String newJwtToken = jwtGenerator.generate(user);
        user.setToken(Optional.of(newJwtToken));
        userRepository.update(user);
        return newJwtToken;
    }

    public void register(User user) throws NotUniqueEntityException {
        userRepository.add(user);
    }

    public void logout(String token) throws NotFoundException {
        User user = userRepository.getByToken(token);
        user.setToken(Optional.empty());
        userRepository.update(user);
    }
}