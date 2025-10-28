using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class HumanResources
    {
        [Key]
        public int HumanResourceId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public byte IsDeleted { get; set; } = 0;
        public string ProfilePhotoUrl { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public int UserAccountId { get; set; }
        public UserAccount? UserAccount { get; set; }
        public List<Application> Applications { get; set; }//xxxx
        public List<Interview>? Interviews { get; set; }
        public List<JobPosting>? JobPostings { get; set; }
        public List<Applicant>? Applicants { get; set; }
    }
}
