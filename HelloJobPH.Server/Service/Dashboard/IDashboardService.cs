namespace HelloJobPH.Server.Service.Dashboard
{
    public interface IDashboardService
    {
        Task<int> GetTotalJobPostAsync();
        Task<int> GetActiveApplicationAsync();
        Task<int> GetApprovedHiredAsync();
        Task<List<int>> GetMonthlyApplicantsAsync();
    }
}
