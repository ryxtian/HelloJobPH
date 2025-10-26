using HelloJobPH.Server.Data;
using HelloJobPH.Server.Service.Auth;
using HelloJobPH.Shared.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HelloJobPH.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _context.UserAccount.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                throw new Exception("User not found.");

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new Exception("Invalid password.");

            var hr = await _context.HumanResource.FirstOrDefaultAsync(h => h.UserAccountId == user.UserAccountId);
       
            if (hr == null)
                throw new Exception("User details not found.");

            if (hr.IsDeleted == 1)
                throw new Exception("Account has been deleted or disabled.");

            return CreateToken(user, hr);
        }

        public string CreateToken(UserAccount user, HumanResources hr)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, hr.HumanResourceId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, hr.Firstname),
                new Claim(ClaimTypes.Surname, hr.Lastname),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppSettings:Token"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
