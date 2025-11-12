using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class JobPostAudit
    {
        public int Id { get; set; }

        public string Action { get; set; } = ""; // e.g., "Created", "Updated", "Deleted"
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
        public int? EmployerId { get; set; }
        public Employer? Employer { get; set; }
        public int JobPostingId { get; set; }
        public JobPosting? JobPosting { get; set; }
        public int? HumanResourceId { get; set; }
        public HumanResources? HumanResource { get; set; }
    }
}
