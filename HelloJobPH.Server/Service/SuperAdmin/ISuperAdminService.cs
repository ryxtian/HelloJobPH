using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
using static HelloJobPH.Employer.Pages.SuperAdmin.AdminDashboard;

namespace HelloJobPH.Server.Service.SuperAdmin
{
    public interface ISuperAdminService
    {
        //For Employer List >
        Task<List<EmployerListDtos>> EmployersList();
        Task<List<EmployerListDtos>> ForApprovalList();

        Task<List<JobPostingListDtos>> JobPostList();
        Task<List<ApplicantDtos>> ApplicantList();

        //For Approval
        Task<bool> ApprovedEmployer(int id);
        Task<bool> DisableEmployer(int id);
        Task<bool> DisapprovedEmployer(int id);

        //Dashboard

        Task<int> TotalJobCounts();
        Task<int> TotalApplicants();
        Task<int> ScheduledInterviews();
        Task<int> SuccessfulHires();
        Task<List<TopJobDto>> GetTopAppliedJobs();
        Task<List<ApplicationStatusDto>> GetApplicationStatusCounts();


        Task<bool> BlockApplicantAsync(int id);
        Task<bool> UnBlockApplicantAsync(int id);
        Task<ApplicantDtos?> ViewApplicantAsync(int id);
    }
}
