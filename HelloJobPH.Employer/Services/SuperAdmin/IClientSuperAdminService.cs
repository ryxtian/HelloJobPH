using HelloJobPH.Shared.DTOs;
using static HelloJobPH.Employer.Pages.SuperAdmin.AdminDashboard;

namespace HelloJobPH.Employer.Services.SuperAdmin
{
    public interface IClientSuperAdminService
    {


        Task<List<EmployerListDtos>> EmployerList();
        Task<List<EmployerListDtos>> ForApprovalList();


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




        Task<bool> BlockApplicant(int id);
        Task<bool> UnBlockApplicant(int id);
        Task<ApplicantViewDtos> ViewApplicant(int id);


        Task<bool> BlockJob(int id);
        Task<bool> UnBlockJob(int id);


        Task<List<JobPostingListDtos>> JobPostList();
        Task<JobPostingDtos> GetJobDetails(int id);
    }
}
