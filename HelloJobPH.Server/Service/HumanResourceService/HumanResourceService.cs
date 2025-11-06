using AutoMapper;
using HelloJobPH.Server.Data;
using HelloJobPH.Server.Utility;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace HelloJobPH.Server.Service.HumanResource
{
    public class HumanResourceService : IHumanResourceService
    {
        private readonly ApplicationDbContext _context;

        public HumanResourceService(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<bool> AddAsync(HumanResourceDtos humanResourceDto)
        {

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(humanResourceDto.Password);
            var useracc = new UserAccount
            {
                Email = humanResourceDto.Email,
                Password = passwordHash,
                Role = "Admin"
            };

            await _context.UserAccount.AddAsync(useracc);
            await _context.SaveChangesAsync();
            var entity = new HumanResources
            {
                Firstname = humanResourceDto.Firstname,
                Lastname = humanResourceDto.Lastname,
                PhoneNumber = humanResourceDto.PhoneNumber,
                IsDeleted = humanResourceDto.IsDeleted,
                ProfilePhotoUrl = humanResourceDto.ProfilePhotoUrl,
                JobTitle = humanResourceDto.JobTitle,
                UserAccountId = useracc.UserAccountId,
                //Email = useracc.Email,
            };

            await _context.HumanResource.AddAsync(entity);
            await _context.SaveChangesAsync();



            return true;
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

        public async Task<HumanResourceDtos> GetByIdAsync(int id)
        {
            var entity = await _context.HumanResource
                      .Include(hr => hr.UserAccount).FirstOrDefaultAsync(i => i.HumanResourceId == id);
      
            if (entity == null)
                return null; // or throw exception, depending on your design

            return new HumanResourceDtos
            {
                HumanResourceId = entity.HumanResourceId,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                Email = entity.UserAccount.Email,
                PhoneNumber = entity.PhoneNumber,
                IsDeleted = entity.IsDeleted,
                ProfilePhotoUrl = entity.ProfilePhotoUrl,
                JobTitle = entity.JobTitle
            };

        }

        public async Task<List<HumanResourceDtos>> RetrieveAllAsync()
        {
            var userId = Utilities.GetUserId();

            
            try
            {
                var response = await _context.HumanResource
                    .Where(hr => hr.IsDeleted == 0 && hr.EmployerId == userId)
                    .Select(hr => new HumanResourceDtos
                    {
                        HumanResourceId = hr.HumanResourceId,
                        Firstname = hr.Firstname,
                        Lastname = hr.Lastname,
                        PhoneNumber = hr.PhoneNumber,
                        IsDeleted = hr.IsDeleted,
                        ProfilePhotoUrl = hr.ProfilePhotoUrl,
                        JobTitle = hr.JobTitle,

                        Email = hr.UserAccount.Email,

                    })
                    .ToListAsync();

                return response;
            }
            catch (Exception)
            {
                // Optionally log the exception
                throw;
            }
        }



        public async Task<bool> UpdateAsync(HumanResourceDtos humanResourceDto)
        {

            var existing = await _context.HumanResource
                .FirstOrDefaultAsync(hr => hr.HumanResourceId == humanResourceDto.HumanResourceId);

            var useracc = await _context.UserAccount
       .FirstOrDefaultAsync(u => u.UserAccountId == existing.UserAccountId);

            if (existing == null || useracc == null)
                return false;


            string passwordHash = BCrypt.Net.BCrypt.HashPassword(humanResourceDto.Password);

            useracc.Email = humanResourceDto.Email;
            useracc.Password = passwordHash;
           

            _context.UserAccount.Update(useracc);
            await _context.SaveChangesAsync();

            // Map fields from DTO to entity
            existing.Firstname = humanResourceDto.Firstname;
            existing.Lastname = humanResourceDto.Lastname;
            //existing.Email = humanResourceDto.Email;
            existing.PhoneNumber = humanResourceDto.PhoneNumber;
            existing.IsDeleted = humanResourceDto.IsDeleted;
            existing.ProfilePhotoUrl = humanResourceDto.ProfilePhotoUrl;
            existing.JobTitle = humanResourceDto.JobTitle;
            existing.UserAccountId = useracc.UserAccountId;

            _context.HumanResource.Update(existing);
            await _context.SaveChangesAsync();

            // Return updated DTO
            return true;
        }


    }
}
