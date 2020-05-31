package com.ticketflow.api_gateway.proxy.profile;

import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.ResponseBody;

import java.util.List;

import com.ticketflow.api_gateway.models.exceptions.NotFoundException;
import com.ticketflow.api_gateway.models.profile_service.Profile;

@FeignClient(name = "${profile.service.name}", configuration = ProfileFeignConfiguration.class)
interface ProfilesFeignClient {
    @ResponseBody
    @GetMapping(value = "/")
    public ResponseEntity<String> home();

    @ResponseBody
    @GetMapping(value = "/profiles")
    public ResponseEntity<List<Profile>> getAll();

    @ResponseBody
    @GetMapping(value = "/profiles/by-id/{id}")
    public ResponseEntity<Profile> getById(@PathVariable Integer id) throws NotFoundException;

    @ResponseBody
    @PostMapping(value = "/profiles/by-user")
    public ResponseEntity<Profile> getByUserEmail(@RequestBody String userEmail) throws NotFoundException;

    @ResponseBody
    @PostMapping(value = "/profiles")
    public ResponseEntity<String> add(@RequestBody Profile profile);

    @ResponseBody
    @PutMapping(value = "/profiles/{id}")
    public ResponseEntity<String> update(@PathVariable Integer id, @RequestBody Profile profile) throws NotFoundException;

    @ResponseBody
    @DeleteMapping(value = "/profiles/{id}")
    public ResponseEntity<String> delete(@PathVariable Integer id) throws NotFoundException;
}