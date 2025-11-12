using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class Employer
    {
        [Key]
        public int EmployerId { get; set; }

        [Required, MaxLength(150)]
        public string CompanyName { get; set; } = string.Empty;

        public string Industry { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string CompanyAddress { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";

        public string Province { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        [EmailAddress]
        public string ContactEmail { get; set; } = string.Empty;

        [MaxLength(20)]
        public string ContactNumber { get; set; } = string.Empty;

        [MaxLength(255)]
        [Url]
        public string Website { get; set; } = string.Empty;

        public int UserAccountId { get; set; }
        public List<JobPosting>? JobPost { get; set; }
        public List<HumanResources>? HumanResources { get; set; }
        public List<AuditLog>? AuditLogs { get; set; }
        public List<JobPostAudit>? JobPostAudits { get; set; }
        public List<Application>? Applications { get; set; }
        public UserAccount? UserAccount { get; set; }
        public DateTime DateRegistered { get; set; } = DateTime.UtcNow;

        public bool IsVerified { get; set; } = false;

        public bool IsActive { get; set; } = false;
        public byte IsDeleted { get; set; } = 0;
    }
}
