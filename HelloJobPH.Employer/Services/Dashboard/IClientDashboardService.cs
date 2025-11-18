namespace HelloJobPH.Employer.Services.Dashboard
{
    public interface IClientDashboardService
    {
        Task<int> GetTotalJobPostAsync();
        Task<int> GetActiveApplicationAsync();
        Task<int> GetApprovedHiredAsync();
        Task<List<int>> GetMonthlyApplicantsAsync();
        
    }
}
