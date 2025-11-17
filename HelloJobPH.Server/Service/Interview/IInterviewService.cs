using HelloJobPH.Server.GeneralReponse;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Enums;

namespace HelloJobPH.Server.Service.Interview
{
    public interface IInterviewService
    {
        Task<List<InterviewListDtos>> InitialList();
        Task<List<InterviewListDtos>> TechnicalList();
        Task<List<InterviewListDtos>> FinalList();
        Task<GeneralResponse<bool>> Reschedule(SetScheduleDto dto);
        Task<GeneralResponse<int>> NoAppearance(int id);
        Task<GeneralResponse<bool>> ForTechnical(SetScheduleDto dto);
        Task<GeneralResponse<bool>> ForFinal(SetScheduleDto dto);
        Task<GeneralResponse<bool>> Failed(int id);
         Task<GeneralResponse<bool>> DeleteApplication(int id);
        Task<GeneralResponse<bool>> MarkAsCompleted(int id);

        Task<List<InterviewerDtos>> InterviewerList();
    }
}
