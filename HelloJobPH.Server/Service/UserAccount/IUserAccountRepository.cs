using HelloJobPH.Shared.Model;

namespace HelloJobPH.Server.Service.UserAccountRepository
{
    public interface IUserAccountRepository
    {
        Task<List<UserAccount>> RetrieveAllAsync();
        Task<UserAccount> GetByIdAsync(int id);
        Task<UserAccount> GetByEmailAsync(string email);
        Task AddAsync(UserAccount entity);
        void Update(UserAccount entity);
        Task SaveChangesAsync();
    }
}
