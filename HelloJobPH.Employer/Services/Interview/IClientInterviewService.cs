using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Enums;

namespace HelloJobPH.Employer.Services.Interview
{
    public interface IClientInterviewService
    {
        Task<List<InterviewListDtos>> InitialList();
        Task<List<InterviewListDtos>> TechnicalList();
        Task<List<InterviewListDtos>> FinalList();
        Task<bool> Reschedule(SetScheduleDto dto);
        Task<int> NoAppearance(int id);
        Task<bool> ForTechnical(SetScheduleDto dto);
        Task<bool> ForFinal(SetScheduleDto dto);
        Task<int> Failed(int id);
        Task<int> Delete(int id);
        Task<int> MarkAsCompleted(int id);

        Task<List<InterviewerDtos>> RetrieveAllInterviewer();
    }
}
