﻿using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Employer.Services.Candidate
{
    public interface IClientCandidateService
    {
        Task<bool> CandidateAcceptAsync(int id);
        Task<bool> CandidateRejectAsync(int id);
        Task<List<ApplicationListDtos>> RetrieveAllCandidate();
    }
}
