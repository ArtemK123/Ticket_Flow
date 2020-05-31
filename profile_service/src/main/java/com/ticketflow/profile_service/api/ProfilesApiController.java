package com.ticketflow.profile_service.api;

import java.util.List;

import com.ticketflow.profile_service.models.Profile;
import com.ticketflow.profile_service.models.exceptions.NotFoundException;
import com.ticketflow.profile_service.service.ProfileService;

import org.springframework.http.ResponseEntity;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class ProfilesApiController {
    private ProfileService profileService;

    @Autowired
    public ProfilesApiController(ProfileService profileService) {
        this.profileService = profileService;
    }

    @ResponseBody
    @GetMapping(value = "/")
    public ResponseEntity<String> home() {
        return ResponseEntity.ok().body("<span>Hello from TicketFlow Profile service</span>");
    }

    @ResponseBody
    @GetMapping(value = "/profiles")
    public ResponseEntity<List<Profile>> getAll() {
        List<Profile> profiles = this.profileService.getAll();
        return ResponseEntity.ok().body(profiles);
    }

    @ResponseBody
    @GetMapping(value = "/profiles/by-id/{id}")
    public ResponseEntity<Profile> getById(@PathVariable Integer id) throws NotFoundException {
        Profile profile = this.profileService.getById(id);
        return ResponseEntity.ok().body(profile);
    }

    @ResponseBody
    @PostMapping(value = "/profiles/by-user")
    public ResponseEntity<Profile> getByUserEmail(@RequestBody String userEmail) throws NotFoundException {
        Profile profile = this.profileService.getByUserEmail(userEmail);
        return ResponseEntity.ok().body(profile);
    }

    @ResponseBody
    @PostMapping(value = "/profiles")
    public ResponseEntity<String> add(@RequestBody Profile profile) {
        Integer createdProfileId = this.profileService.add(profile);
        return ResponseEntity.status(201).body(String.format("Added successfully. Id - %d", createdProfileId));
    }

    @ResponseBody
    @PutMapping(value = "/profiles/{id}")
    public ResponseEntity<String> update(@PathVariable Integer id, @RequestBody Profile profile) throws NotFoundException {
        this.profileService.update(id, profile);
        return ResponseEntity.accepted().body("Updated successfully");
    }

    @ResponseBody
    @DeleteMapping(value = "/profiles/{id}")
    public ResponseEntity<String> delete(@PathVariable Integer id) throws NotFoundException {
        this.profileService.delete(id);
        return ResponseEntity.ok().body("Deleted sucessfully");
    }
}