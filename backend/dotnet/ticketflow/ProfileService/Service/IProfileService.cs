﻿using ProfileService.Models;

namespace ProfileService.Service
{
    public interface IProfileService
    {
        Profile GetById(int id);

        Profile GetByUserEmail(string email);

        int Add(Profile profile);
    }
}