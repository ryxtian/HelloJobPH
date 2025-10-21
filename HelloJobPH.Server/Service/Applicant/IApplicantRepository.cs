using HelloJobPH.Shared.Model;
namespace HelloJobPH.Server.Service.ApplicantRepo
{
    public interface IApplicantRepository
    {
        Task<List<Applicant>> RetrieveAllAsync();
        Task<Applicant> GetByIdAsync(int id);
        Task AddAsync(Applicant entity);
        void Update(Applicant entity);
        Task SaveChangesAsync();
    }
}
