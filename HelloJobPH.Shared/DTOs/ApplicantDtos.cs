using HelloJobPH.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class ApplicantDtos
    {
        public int ApplicantId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Middlename { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public byte IsDeleted { get; set; }
        public string Email { get; set; } = string.Empty;
        public UserAccount? UserAccount { get; set; }
        public string FullName => $"{Firstname} {(!string.IsNullOrWhiteSpace(Middlename) ? Middlename + " " : "")}{Surname}";
    }
}
