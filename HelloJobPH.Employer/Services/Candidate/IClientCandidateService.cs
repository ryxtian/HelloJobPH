using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Employer.Services.Candidate
{
    public interface IClientCandidateService
    {
        Task<ApplicationListDtos> CandidateAcceptAsync(int id);
        Task<bool> CandidateRejectAsync(int id);
        Task<List<ApplicationListDtos>> RetrieveAllCandidate();
        Task<List<ApplicationListDtos>> RetrieveAllAcceptedCandidate();

        Task<bool> ForInitial(int applicationId, string interviewDate, string interviewTime, string? location);
    
    }
}
