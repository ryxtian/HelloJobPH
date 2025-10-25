﻿using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Employer.Services.Authentication
{
    public interface IClientIAuthService
    {
        Task<bool> LoginAsync(LoginDtos user);
       // Task<IEnumerable<ClaimsDtos>> GetClaims();
    }


}
