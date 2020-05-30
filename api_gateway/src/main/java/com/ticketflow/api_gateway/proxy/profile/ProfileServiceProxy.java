package com.ticketflow.api_gateway.proxy.profile;

import java.util.List;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.profile_service.Profile;

import org.springframework.stereotype.Component;

@Component
public class ProfileServiceProxy {

    public List<Profile> getAll() {
        throw new UnsupportedOperationException("ProfileServiceProxy.getAll is called");
    }

    public Profile getByUserEmail(String userEmail) throws NotFoundException {
        throw new UnsupportedOperationException("ProfileServiceProxy.get is called");
    }

    public String add(Profile profile) {
        throw new UnsupportedOperationException("ProfileServiceProxy.add is called");
    }

    public String update(Integer id, Profile profile) throws NotFoundException {
        throw new UnsupportedOperationException("ProfileServiceProxy.update is called");
    }

    public String delete(Integer id) throws NotFoundException {
        throw new UnsupportedOperationException("ProfileServiceProxy.delete is called");
    }
}