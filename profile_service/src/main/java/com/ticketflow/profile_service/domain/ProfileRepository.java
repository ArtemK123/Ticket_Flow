package com.ticketflow.profile_service.domain;

import java.util.List;
import java.util.stream.Collectors;

import com.ticketflow.profile_service.models.Profile;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

@Repository
public class ProfileRepository {
    
    private ProfileJpaRepository profileJpaRepository;

    @Autowired
    public ProfileRepository(ProfileJpaRepository profileJpaRepository) {
        this.profileJpaRepository = profileJpaRepository;
    }

    public List<Profile> getAll() {
        List<ProfileDatabaseModel> profilesInDatabase = profileJpaRepository.findAll();
        return profilesInDatabase.stream().map(this::convertToProfile).collect(Collectors.toList());
    }

    public Profile get(int id) {
        return convertToProfile(profileJpaRepository.getOne(id));
    }

    public int add(Profile profile) {
        ProfileDatabaseModel profileDatabaseModel = convertToProfileDatabaseModel(profile);
        profileJpaRepository.saveAndFlush(profileDatabaseModel);
        return profileDatabaseModel.getId();
    }

    public void update(int id, Profile updatedProfile) {
        ProfileDatabaseModel profileDatabaseModel = profileJpaRepository.getOne(id);
        profileDatabaseModel.setBirthday(updatedProfile.getBirthday());
        profileDatabaseModel.setPhoneNumber(updatedProfile.getPhoneNumber());
        profileDatabaseModel.setUserEmail(updatedProfile.getUserEmail());

        profileJpaRepository.saveAndFlush(profileDatabaseModel);
    }

    public void delete(int id) {
        profileJpaRepository.deleteById(id);
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