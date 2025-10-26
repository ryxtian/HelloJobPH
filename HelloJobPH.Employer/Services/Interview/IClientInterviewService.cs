using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Employer.Services.Interview
{
    public interface IClientInterviewService
    {
        Task<List<InterviewListDtos>> InitialList();
        Task<List<InterviewListDtos>> TechnicalList();
        Task<List<InterviewListDtos>> FinalList();
    }
}
