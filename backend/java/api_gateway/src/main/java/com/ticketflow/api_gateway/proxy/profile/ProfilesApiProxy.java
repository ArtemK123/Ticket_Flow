package com.ticketflow.api_gateway.proxy.profile;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.profile_service.Profile;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class ProfilesApiProxy {

    private ProfilesApiFeignClient profilesFeignClient;

    @Autowired
    public ProfilesApiProxy(ProfilesApiFeignClient profilesFeignClient) {
        this.profilesFeignClient = profilesFeignClient;
    }

    public Profile getByUserEmail(String userEmail) throws NotFoundException {
        return profilesFeignClient.getByUserEmail(userEmail).getBody();
    }

    public void add(Profile profile) {
        profilesFeignClient.add(profile).getBody();
    }
}