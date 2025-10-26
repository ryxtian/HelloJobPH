using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Server.Service.Interview
{
    public interface IInterviewService
    {
        Task<List<InterviewListDtos>> InitialList();
        Task<List<InterviewListDtos>> TechnicalList();
        Task<List<InterviewListDtos>> FinalList();
    }
}
