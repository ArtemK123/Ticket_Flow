package com.ticketflow.profile_service.service;

import java.util.List;

import com.ticketflow.profile_service.domain.ProfileRepository;
import com.ticketflow.profile_service.models.Profile;
import com.ticketflow.profile_service.models.exceptions.NotFoundException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class ProfileService {
    private static final String NOT_FOUND_EXCEPTION_MESSAGE = "Profile with id=%d is not found";
    
    private ProfileRepository profileRepository;

    @Autowired
    public ProfileService(ProfileRepository profileRepository) {
        this.profileRepository = profileRepository;
    }

    public List<Profile> getAll() {
        return this.profileRepository.getAll();
    }

    public Profile get(int id) throws NotFoundException {
        Profile product = this.profileRepository.get(id);
        if (product == null) {
            throw new NotFoundException(String.format(NOT_FOUND_EXCEPTION_MESSAGE, id));            
        }

        return product;
    }

    public int add(Profile profile) {
        return this.profileRepository.add(profile);
    }

    public void update(int id, Profile updatedProfile) throws NotFoundException {

        Profile storedProfile = this.profileRepository.get(id);
        if (storedProfile == null) {
            throw new NotFoundException(String.format(NOT_FOUND_EXCEPTION_MESSAGE, id));   
        }

        this.profileRepository.update(id, updatedProfile);
    }

    public void delete(int id) throws NotFoundException {
        Profile storedProfile = this.profileRepository.get(id);
        if (storedProfile == null) {
            throw new NotFoundException(String.format(NOT_FOUND_EXCEPTION_MESSAGE, id));
        }

        this.profileRepository.delete(id);
    }
}