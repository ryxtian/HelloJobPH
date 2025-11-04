//using HelloJobPH.Server.Data;
//using HelloJobPH.Server.Service.Auth;
//using HelloJobPH.Shared.Model;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace HelloJobPH.Server.Services
//{
//    public class AuthService : IAuthService
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly IConfiguration _config;

//        public AuthService(ApplicationDbContext context, IConfiguration config)
//        {
//            _context = context;
//            _config = config;
//        }

//        public async Task<string> LoginAsync(string email, string password)
//        {
//            var user = await _context.UserAccount.FirstOrDefaultAsync(u => u.Email == email);

//            if (user == null)
//                throw new Exception("User not found.");

//            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
//                throw new Exception("Invalid password.");

//            var hr = await _context.HumanResource.FirstOrDefaultAsync(h => h.UserAccountId == user.UserAccountId);

//            if (hr == null)
//                throw new Exception("User details not found.");

//            if (hr.IsDeleted == 1)
//                throw new Exception("Account has been deleted or disabled.");

//            return CreateToken(user, hr);
//        }

//        public string CreateToken(UserAccount user, HumanResources hr)
//        {
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.NameIdentifier, hr.HumanResourceId.ToString()),
//                new Claim(ClaimTypes.Email, user.Email),
//                new Claim(ClaimTypes.Name, hr.Firstname),
//                new Claim(ClaimTypes.Surname, hr.Lastname),
//                new Claim(ClaimTypes.Role, user.Role)
//            };

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppSettings:Token"]!));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

//            var token = new JwtSecurityToken(
//                claims: claims,
//                expires: DateTime.Now.AddDays(1),
//                signingCredentials: creds
//            );

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}



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
            try
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

                if (hr.EmployerId != null)
                {
                    var employer = await _context.Employers
                        .FirstOrDefaultAsync(e => e.EmployerId == hr.EmployerId);

                    if (employer == null)
                        throw new Exception("Associated employer not found.");

                    if (employer.IsDeleted == 1 || employer.Status == "Disable")
                        throw new Exception("Your employer account is disabled. Login not allowed.");
                }


                //if (user.Role == "Employer")
                //{
                //    var employer = await _context.Employers.FirstOrDefaultAsync(e => e.UserAccountId == user.UserAccountId);
                //    if (employer == null)
                //        throw new Exception("Employer account not found.");

                //    if (employer.IsDeleted == 1 || employer.Status == "Disable")
                //        throw new Exception("Your employer account is disabled or has been removed.");
                //}

                // ✅ Claims for the logged-in user
                var claims = new[]
                {
            new Claim(ClaimTypes.Name, hr.Firstname),
            new Claim(ClaimTypes.Surname, hr.Lastname),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.NameIdentifier, user.UserAccountId.ToString())
        };

                // ✅ Use the same JWT settings as Program.cs
                var jwtKey = _config["Jwt:Key"];
                var jwtIssuer = _config["Jwt:Issuer"];
                var jwtAudience = _config["Jwt:Audience"];

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: jwtIssuer,
                    audience: jwtAudience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception)
            {

                throw;
            }
            
        }





        public string CreateToken(UserAccount user, HumanResources hr)
        {
            try
            {
                var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, hr.HumanResourceId.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Name, hr.Firstname),
        new Claim(ClaimTypes.Surname, hr.Lastname),
        new Claim(ClaimTypes.Role, user.Role)
    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
