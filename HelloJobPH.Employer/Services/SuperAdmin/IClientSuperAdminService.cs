using HelloJobPH.Shared.DTOs;
using static HelloJobPH.Employer.Pages.SuperAdmin.AdminDashboard;

namespace HelloJobPH.Employer.Services.SuperAdmin
{
    public interface IClientSuperAdminService
    {


        Task<List<EmployerListDtos>> EmployerList();
        Task<List<EmployerListDtos>> ForApprovalList();
        Task<List<JobPostingListDtos>> JobPostList();

        Task<List<ApplicantDtos>> ApplicantList();



        Task<bool> ApprovedEmployer(int id);
        Task<bool> DisapprovedEmployer(int id);
        Task<bool> DisableEmployer(int id);



        Task<int> TotalJobCounts();
        Task<int> TotalApplicants();
        Task<int> ScheduledInterviews();
        Task<int> SuccessfulHires();

        Task<List<TopJobDto>> GetTopAppliedJobs();
        Task<List<ApplicationStatusDto>> GetApplicationStatusCounts();



        //user account

        Task<bool> BlockApplicant();
        Task<bool> UnBlockApplicant();
        Task<ApplicantDtos> ViewApplicant(int id);


    }
}
