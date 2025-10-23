using AutoMapper;
using HelloJobPH.Server.Data;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
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
        public async Task<HumanResourceDtos> AddAsync(HumanResourceDtos humanResourceDto)
        {
            var entity = new HumanResources
            {
                Firstname = humanResourceDto.Firstname,
                Lastname = humanResourceDto.Lastname,
                Email = humanResourceDto.Email,
                PhoneNumber = humanResourceDto.PhoneNumber,
                IsDeleted = humanResourceDto.IsDeleted,
                ProfilePhotoUrl = humanResourceDto.ProfilePhotoUrl,
                JobTitle = humanResourceDto.JobTitle
            };

            await _context.HumanResource.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new HumanResourceDtos
            {
                HumanResourceId = entity.HumanResourceId,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                IsDeleted = entity.IsDeleted,
                ProfilePhotoUrl = entity.ProfilePhotoUrl,
                JobTitle = entity.JobTitle
            };
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
            var entity = await _context.HumanResource.FirstOrDefaultAsync(i => i.HumanResourceId == id);

            if (entity == null)
                return null; // or throw exception, depending on your design

            return new HumanResourceDtos
            {
                HumanResourceId = entity.HumanResourceId,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                IsDeleted = entity.IsDeleted,
                ProfilePhotoUrl = entity.ProfilePhotoUrl,
                JobTitle = entity.JobTitle
            };

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


        public async Task<HumanResourceDtos?> UpdateAsync(HumanResourceDtos humanResourceDto)
        {
            var existing = await _context.HumanResource
                .FirstOrDefaultAsync(hr => hr.HumanResourceId == humanResourceDto.HumanResourceId);

            if (existing == null)
                return null; // or throw new Exception("Human resource not found");

            // Map fields from DTO to entity
            existing.Firstname = humanResourceDto.Firstname;
            existing.Lastname = humanResourceDto.Lastname;
            existing.Email = humanResourceDto.Email;
            existing.PhoneNumber = humanResourceDto.PhoneNumber;
            existing.IsDeleted = humanResourceDto.IsDeleted;
            existing.ProfilePhotoUrl = humanResourceDto.ProfilePhotoUrl;
            existing.JobTitle = humanResourceDto.JobTitle;

            _context.HumanResource.Update(existing);
            await _context.SaveChangesAsync();

            // Return updated DTO
            return new HumanResourceDtos
            {
                HumanResourceId = existing.HumanResourceId,
                Firstname = existing.Firstname,
                Lastname = existing.Lastname,
                Email = existing.Email,
                PhoneNumber = existing.PhoneNumber,
                IsDeleted = existing.IsDeleted,
                ProfilePhotoUrl = existing.ProfilePhotoUrl,
                JobTitle = existing.JobTitle
            };
        }


    }
}
