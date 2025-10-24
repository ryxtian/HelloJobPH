using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class RegisterHRDtos
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public string ProfileUrl { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
    }
}
