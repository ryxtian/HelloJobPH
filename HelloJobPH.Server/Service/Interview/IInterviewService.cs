using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Enums;

namespace HelloJobPH.Server.Service.Interview
{
    public interface IInterviewService
    {
        Task<List<InterviewListDtos>> InitialList();
        Task<List<InterviewListDtos>> TechnicalList();
        Task<List<InterviewListDtos>> FinalList();
        Task<bool> Reschedule(SetScheduleDto dto);
        Task<int> NoAppearance(int id);
        Task<bool> ForTechnical(SetScheduleDto dto);
        Task<bool> ForFinal(SetScheduleDto dto);
        Task<bool> Failed(int id);
         Task<bool> DeleteApplication(int id);
        Task<bool> MarkAsCompleted(int id);
    }
}
