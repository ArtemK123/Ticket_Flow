package com.ticketflow.api_gateway.models.client_models.user_api;

import com.ticketflow.api_gateway.models.profile_service.Profile;

public class ProfileResponseData {
    private String userEmail;
    private ProfileClientModel profile;

    public ProfileResponseData(Profile profile) {
        this.userEmail = profile.getUserEmail();
        this.profile = new ProfileClientModel(profile.getPhoneNumber(), profile.getBirthday());
    }
    
    public String getUserEmail() {
        return userEmail;
    }

    public ProfileClientModel getProfile() {
        return this.profile;
    }
}