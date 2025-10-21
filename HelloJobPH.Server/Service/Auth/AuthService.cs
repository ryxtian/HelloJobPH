using HelloJobPH.Server.Data;
using HelloJobPH.Server.Service.Auth;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HelloJobPH.Employer.Services.Repository
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration, ApplicationDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        public Task<string?> GetToken()
        {
            throw new NotImplementedException();
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _context.UserAccount.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                throw new Exception("User Not Found.");

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new Exception("Wrong password.");

            var applicant = await _context.Applicant.FirstOrDefaultAsync(a => a.ApplicantId == user.ApplicantId);

            if (applicant == null)
                throw new Exception("Applicant details not found.");

            return CreateToken(user, applicant);
        }
        public async Task<string> RegisterAsync(RegisterDtos register)
        {
            if (await _context.UserAccount.FirstOrDefaultAsync(x=>x.Email == register.Email) is not null)
                throw new Exception("Email is already registered.");
            var applicant = new Applicant
            {
                Firstname = register.Firstname,
                Middlename = register.Middlename,
                Surname = register.Surname,
                Address = register.Address,
                Phone = register.Phone,
                Birthday = register.Birthday,
            };

            await _context.Applicant.AddAsync(applicant);
            await _context.SaveChangesAsync(); 

           
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(register.PasswordHash);

         
            var userAccount = new UserAccount
            {
                Email = register.Email,
                Password = passwordHash,
                ConfirmPassword = passwordHash,
                Role = "Applicant",
                ApplicantId = applicant.ApplicantId
            };

            await _context.UserAccount.AddAsync(userAccount);
            await _context.SaveChangesAsync();

            return CreateToken(userAccount, applicant);
        }

        public string CreateToken(UserAccount user, Applicant applicantdetail)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, applicantdetail.ApplicantId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, applicantdetail.Firstname),
                new Claim(ClaimTypes.Surname, applicantdetail.Surname),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
