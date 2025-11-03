using HelloJobPH.Server.Data;
using HelloJobPH.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HelloJobPH.Server.Service.SuperAdmin
{
    public class SuperAdminService : ISuperAdminService
    {
        private readonly ApplicationDbContext _context;
        public SuperAdminService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<EmployerListDtos>> EmployersList()
        {

            List<EmployerListDtos> employers = await _context.Employers
                .Select(e => new EmployerListDtos
                {
                    EmployerId = e.EmployerId,
                    CompanyName = e.CompanyName,
                    Industry = e.Industry,
                    Description = e.Description,
                    CompanyAddress = e.CompanyAddress,
                    City = e.City,
                    Province = e.Province,
                    ContactEmail = e.ContactEmail,
                    ContactNumber = e.ContactNumber
                })
                .ToListAsync();

            return employers;
        }

    }
}
