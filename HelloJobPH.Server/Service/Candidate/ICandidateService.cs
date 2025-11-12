using HelloJobPH.Server.GeneralReponse;
using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Server.Service.Candidate
{
    public interface ICandidateService
    {
        Task<GeneralResponse<bool>> CandidateAccepttAsync(int id);
        Task<GeneralResponse<bool>> CandidateRejectAsync(int id);
        Task<GeneralResponse<bool>> SendInitialEmail(SetScheduleDto dto);
        Task<List<ApplicationListDtos>> RetrieveAllCandidate();
        Task<List<ApplicationListDtos>> RetrieveAllAcceptedCandidate();
        Task<GeneralResponse<bool>> ViewResumeUpdate(int id);
    }
}
