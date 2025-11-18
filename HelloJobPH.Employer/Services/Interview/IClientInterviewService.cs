using HelloJobPH.Employer.GeneralResponse;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Enums;

namespace HelloJobPH.Employer.Services.Interview
{
    public interface IClientInterviewService
    {
        Task<GeneralResponse<List<InterviewListDtos>>> InitialList();
        //Task<List<InterviewListDtos>> TechnicalList();
        //Task<List<InterviewListDtos>> FinalList();
        Task<GeneralResponse<bool>> Reschedule(SetScheduleDto dto);
        Task<GeneralResponse<bool>> NoAppearance(int id);
        Task<GeneralResponse<bool>> ForTechnical(SetScheduleDto dto);
        Task<GeneralResponse<bool>> ForFinal(SetScheduleDto dto);
        Task<GeneralResponse<bool>> Failed(int id);
        Task<GeneralResponse<bool>> Delete(int id);
        Task<GeneralResponse<bool>> MarkAsCompleted(int id);

        Task<GeneralResponse<bool>> Hired(int id);
        Task<List<InterviewerDtos>> RetrieveAllInterviewer();
    }
}
