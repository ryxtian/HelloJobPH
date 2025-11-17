using HelloJobPH.Shared.DTOs;
using static HelloJobPH.Employer.Services.Candidate.ClientCandidateService;

namespace HelloJobPH.Employer.Services.Candidate
{
    public interface IClientCandidateService
    {
        Task<bool> CandidateAcceptAsync(int id);
        Task<bool> CandidateRejectAsync(int id);
        Task<List<ApplicationListDtos>> RetrieveAllCandidate();
        Task<List<ApplicationListDtos>> RetrieveAllAcceptedCandidate();
        Task<bool> ForInitial(SetScheduleDto dto);
        Task<OverviewResponse?> AIOverviewAsync(int id);
        Task<bool> ViewResumeStatusAsync(int id);

    }
}
