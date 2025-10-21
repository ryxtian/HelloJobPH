using HelloJobPH.Server.Data;
using HelloJobPH.Shared.Model;
using Microsoft.EntityFrameworkCore;


namespace HelloJobPH.Server.Service.UserAccountRepository
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly ApplicationDbContext _context;
        public UserAccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserAccount>> RetrieveAllAsync()
        {
            return await _context.UserAccount.ToListAsync();
        }

        public async Task<UserAccount> GetByIdAsync(int id)
        {
            return await _context.UserAccount.FindAsync(id);
        }

        public async Task<UserAccount> GetByEmailAsync(string email)
        {
            return await _context.UserAccount
                .FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task AddAsync(UserAccount entity)
        {
            await _context.UserAccount.AddAsync(entity);
        }

        public void Update(UserAccount entity)
        {
            _context.UserAccount.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
