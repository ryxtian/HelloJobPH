using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Server.Service.Candidate
{
    public interface ICandidateService
    {
        Task<bool> CandidateAccepttAsync(int id);
        Task<bool> CandidateRejectAsync(int id);
        Task<bool> SendInitialEmail(SetScheduleDto dto);
        Task<List<ApplicationListDtos>> RetrieveAllCandidate();
        Task<List<ApplicationListDtos>> RetrieveAllAcceptedCandidate();
    }
}
