package com.ticketflow.profile_service.service;

import java.util.List;

import com.ticketflow.profile_service.domain.ProfileRepository;
import com.ticketflow.profile_service.models.Profile;
import com.ticketflow.profile_service.models.exceptions.NotFoundException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class ProfileService {
    private ProfileRepository profileRepository;

    @Autowired
    public ProfileService(ProfileRepository profileRepository) {
        this.profileRepository = profileRepository;
    }

    public List<Profile> getAll() {
        return this.profileRepository.getAll();
    }

    public Profile getById(int id) throws NotFoundException {
        return this.profileRepository.getById(id);
    }

    public Profile getByUserEmail(String userEmail) throws NotFoundException {
        return this.profileRepository.getByUserEmail(userEmail);
    }

    public int add(Profile profile) {
        return this.profileRepository.add(profile);
    }

    public void update(int id, Profile updatedProfile) throws NotFoundException {
        this.profileRepository.update(id, updatedProfile);
    }

    public void delete(int id) throws NotFoundException {
        this.profileRepository.delete(id);
    }
}