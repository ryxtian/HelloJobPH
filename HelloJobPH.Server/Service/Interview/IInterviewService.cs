using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Server.Service.Interview
{
    public interface IInterviewService
    {
        Task<List<InterviewListDtos>> InitialList();
        Task<List<InterviewListDtos>> TechnicalList();
        Task<List<InterviewListDtos>> FinalList();
        Task<bool> Reschedule(int applicationId, string interviewDate, string interviewTime, string? location);
        Task<int> NoAppearance(int id);
        Task<bool> ForTechnical(int applicationId, string interviewDate, string interviewTime, string? location);
        Task<bool> ForFinal(int applicationId, string interviewDate, string interviewTime, string? location);
        Task<bool> Failed(int id);
         Task<bool> DeleteApplication(int id);
    }
}
