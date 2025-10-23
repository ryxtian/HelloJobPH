using AutoMapper;
using HelloJobPH.Server.Data;
using HelloJobPH.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HelloJobPH.Server.Service.HumanResource
{
    public class HumanResourceService : IHumanResourceService
    {
        private readonly ApplicationDbContext _context;

        public HumanResourceService(ApplicationDbContext context)
        {
            _context = context;
   
        }
        public Task<HumanResourceDtos> AddAsync(HumanResourceDtos jobPostingDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.HumanResource
                .FirstOrDefaultAsync(i => i.HumanResourceId == id);

            if (existing == null)
                return false;

            existing.IsDeleted = 1;

            _context.HumanResource.Update(existing);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<HumanResourceDtos> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<HumanResourceDtos>> RetrieveAllAsync()
        {
            try
            {
                List<HumanResourceDtos> response = await _context.HumanResource
                .Select(i => new HumanResourceDtos
                {
                    HumanResourceId= i.HumanResourceId,
                    Firstname = i.Firstname,
                    Lastname = i.Lastname,
                    Email = i.Email,
                    PhoneNumber = i.PhoneNumber,
                    IsDeleted = i.IsDeleted,
                    ProfilePhotoUrl = i.ProfilePhotoUrl,
                    JobTitle = i.JobTitle,
                }).Where(i=>i.IsDeleted ==0)
                .ToListAsync();
                return response;
                //return _mapper.Map<List<HumanResourceDtos>>(response);
            }
            catch (Exception)
            {
                // Log the exception here if you want
                throw;
            }
        }


        public Task<HumanResourceDtos> UpdateAsync(HumanResourceDtos jobPostingDto)
        {
            throw new NotImplementedException();
        }
    }
}
