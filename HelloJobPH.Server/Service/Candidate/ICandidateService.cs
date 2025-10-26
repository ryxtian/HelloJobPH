using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Server.Service.Candidate
{
    public interface ICandidateService
    {
        Task<bool> CandidateAccepttAsync(int id);
        Task<bool> CandidateRejectAsync(int id);
        Task<List<ApplicationListDtos>> RetrieveAllCandidate();
    }
}
