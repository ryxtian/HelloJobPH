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
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte IsDeleted { get; set; } = 0;
        public string ProfilePhotoUrl { get; set; }
        public string JobTitle { get; set; }

        // Add this:
        public int UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }

        public List<JobPosting> JobPostings { get; set; }
        public List<Applicant> Applicants { get; set; }
    }
}
