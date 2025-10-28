using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Employer.Services.Interview
{
    public interface IClientInterviewService
    {
        Task<List<InterviewListDtos>> InitialList();
        Task<List<InterviewListDtos>> TechnicalList();
        Task<List<InterviewListDtos>> FinalList();
        Task<bool> Reschedule(int applicationId, string interviewDate, string interviewTime, string? location);
        Task<int> NoAppearance(int id);
        Task<bool> ForTechnical(int applicationId, string interviewDate, string interviewTime, string? location);
        Task<bool> ForFinal(int applicationId, string interviewDate, string interviewTime, string? location);
        Task<int> Failed(int id);
        Task<int> Delete(int id);
    }
}
