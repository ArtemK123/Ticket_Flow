package com.ticketflow.profile_service.domain;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

import com.ticketflow.profile_service.models.Profile;
import com.ticketflow.profile_service.models.exceptions.NotFoundException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

@Repository
public class ProfileRepository {
    private static final String NOT_FOUND_EXCEPTION_MESSAGE = "Profile with id=%d is not found";
    private ProfileJpaRepository profileJpaRepository;

    @Autowired
    public ProfileRepository(ProfileJpaRepository profileJpaRepository) {
        this.profileJpaRepository = profileJpaRepository;
    }

    public List<Profile> getAll() {
        List<ProfileDatabaseModel> profilesInDatabase = profileJpaRepository.findAll();
        return profilesInDatabase.stream().map(this::convertToProfile).collect(Collectors.toList());
    }

    public Profile get(int id) throws NotFoundException {
        Optional<ProfileDatabaseModel> optionalProfileDatabaseModel = profileJpaRepository.findById(id);
        if (!optionalProfileDatabaseModel.isPresent()) {
            throw new NotFoundException(String.format(NOT_FOUND_EXCEPTION_MESSAGE, id));            
        }

        return convertToProfile(optionalProfileDatabaseModel.get());
    }

    public int add(Profile profile) {
        ProfileDatabaseModel profileDatabaseModel = convertToProfileDatabaseModel(profile);
        profileJpaRepository.saveAndFlush(profileDatabaseModel);
        return profileDatabaseModel.getId();
    }

    public void update(int id, Profile updatedProfile) throws NotFoundException {
        Optional<ProfileDatabaseModel> optionalProfileDatabaseModel = profileJpaRepository.findById(id);
        
        if (!optionalProfileDatabaseModel.isPresent()) {
            throw new NotFoundException(String.format(NOT_FOUND_EXCEPTION_MESSAGE, id));
        }

        ProfileDatabaseModel profileDatabaseModel = optionalProfileDatabaseModel.get();
        profileDatabaseModel.setBirthday(updatedProfile.getBirthday());
        profileDatabaseModel.setPhoneNumber(updatedProfile.getPhoneNumber());
        profileDatabaseModel.setUserEmail(updatedProfile.getUserEmail());

        profileJpaRepository.saveAndFlush(profileDatabaseModel);
    }

    public void delete(int id) throws NotFoundException {
        Optional<ProfileDatabaseModel> optionalProfileDatabaseModel = profileJpaRepository.findById(id);
        if (!optionalProfileDatabaseModel.isPresent()) {
            throw new NotFoundException(String.format(NOT_FOUND_EXCEPTION_MESSAGE, id));
        }

        profileJpaRepository.delete(optionalProfileDatabaseModel.get());
    }

    private Profile convertToProfile(ProfileDatabaseModel profileDatabaseModel) {
        Profile profile = new Profile();
        profile.setId(profileDatabaseModel.getId());
        profile.setPhoneNumber(profileDatabaseModel.getPhoneNumber());
        profile.setUserEmail(profileDatabaseModel.getUserEmail());
        profile.setBirthday(profileDatabaseModel.getBirthday());

        return profile;
    }

    private ProfileDatabaseModel convertToProfileDatabaseModel(Profile profile) {
        ProfileDatabaseModel profileDatabaseModel = new ProfileDatabaseModel();
        profileDatabaseModel.setPhoneNumber(profile.getPhoneNumber());
        profileDatabaseModel.setUserEmail(profile.getUserEmail());
        profileDatabaseModel.setBirthday(profile.getBirthday());

        return profileDatabaseModel;
    }
}