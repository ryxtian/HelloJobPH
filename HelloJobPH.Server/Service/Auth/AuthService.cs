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

            var applicant = await _context.HumanResource.FirstOrDefaultAsync(a => a.UserAccountId == user.UserAccountId);

            if (applicant == null)
                throw new Exception("Applicant details not found.");

            return CreateToken(user, applicant);
        }



        public string CreateToken(UserAccount user, HumanResources HRDetails)
                                          {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, HRDetails.HumanResourceId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, HRDetails.Firstname),
                new Claim(ClaimTypes.Surname, HRDetails.Lastname),
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
