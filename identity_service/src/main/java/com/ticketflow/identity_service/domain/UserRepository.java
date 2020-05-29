package com.ticketflow.identity_service.domain;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

import com.ticketflow.identity_service.models.User;
import com.ticketflow.identity_service.models.exceptions.NotFoundException;
import com.ticketflow.identity_service.models.exceptions.NotUniqueEntityException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

@Repository
public class UserRepository {
    private static final String NOT_FOUND_BY_EMAIL_EXCEPTION_MESSAGE = "User with email=%s is not found";
    private static final String NOT_FOUND_BY_TOKEN_EXCEPTION_MESSAGE = "User with token=%s is not found";
    private static final String NOT_UNIQUE_ENTITY_EXCEPTION_MESSAGE = "User with email=%s already exists";

    private UserJpaRepository userJpaRepository;

    @Autowired
    public UserRepository(UserJpaRepository userJpaRepository) {
        this.userJpaRepository = userJpaRepository;
    }

    public List<User> getAll() {
        List<UserDatabaseModel> profilesInDatabase = userJpaRepository.findAll();
        return profilesInDatabase.stream().map(this::convertToUser).collect(Collectors.toList());
    }

    public User getByEmail(String email) throws NotFoundException {
        Optional<UserDatabaseModel> optionalUserDatabaseModel = userJpaRepository.findByEmail(email);
        if (optionalUserDatabaseModel.isEmpty()) {
            throw new NotFoundException(String.format(NOT_FOUND_BY_EMAIL_EXCEPTION_MESSAGE, email));            
        }

        return convertToUser(optionalUserDatabaseModel.get());
    }

    public User getByToken(String token) throws NotFoundException {
        Optional<UserDatabaseModel> optionalUserDatabaseModel = userJpaRepository.findByToken(token);
        if (optionalUserDatabaseModel.isEmpty()) {
            throw new NotFoundException(String.format(NOT_FOUND_BY_TOKEN_EXCEPTION_MESSAGE, token));            
        }

        return convertToUser(optionalUserDatabaseModel.get());
    }

    public void add(User user) throws NotUniqueEntityException {
        Optional<UserDatabaseModel> optionalUserDatabaseModel = userJpaRepository.findByEmail(user.getEmail());

        if (optionalUserDatabaseModel.isPresent()) {
            throw new NotUniqueEntityException(String.format(NOT_UNIQUE_ENTITY_EXCEPTION_MESSAGE, user.getEmail()));
        }
        
        UserDatabaseModel userDatabaseModel = convertToUserDatabaseModel(user);
        userJpaRepository.saveAndFlush(userDatabaseModel);
    }

    public void update(User updatedUser) throws NotFoundException {
        Optional<UserDatabaseModel> optionalUserDatabaseModel = userJpaRepository.findByEmail(updatedUser.getEmail());
        
        if (optionalUserDatabaseModel.isEmpty()) {
            throw new NotFoundException(String.format(NOT_FOUND_BY_EMAIL_EXCEPTION_MESSAGE, updatedUser.getEmail()));
        }

        UserDatabaseModel userDatabaseModel = optionalUserDatabaseModel.get();
        userDatabaseModel.setPassword(updatedUser.getPassword());
        userDatabaseModel.setRole(updatedUser.getRole());
        userDatabaseModel.setToken(updatedUser.getToken());

        userJpaRepository.saveAndFlush(userDatabaseModel);
    }

    public void delete(String email) throws NotFoundException {
        Optional<UserDatabaseModel> optionalUserDatabaseModel = userJpaRepository.findByEmail(email);
        if (optionalUserDatabaseModel.isEmpty()) {
            throw new NotFoundException(String.format(NOT_FOUND_BY_EMAIL_EXCEPTION_MESSAGE, email));
        }

        userJpaRepository.delete(optionalUserDatabaseModel.get());
    }

    private User convertToUser(UserDatabaseModel userDatabaseModel) {
        return new User(
            userDatabaseModel.getEmail(),
            userDatabaseModel.getPassword(),
            userDatabaseModel.getToken(),
            userDatabaseModel.getRole()
        );
    }

    private UserDatabaseModel convertToUserDatabaseModel(User user) {
        return new UserDatabaseModel(user.getEmail(), user.getPassword(), user.getRole());
    }
}