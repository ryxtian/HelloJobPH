using HelloJobPH.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class JobPosting
    {
        [Key]
        public int JobPostingId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public EmploymentType EmploymentType { get; set; } // e.g., Full-Time, Part-Time, Contract
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
        public byte IsDeleted { get; set; } = 0;
        public JobCategory JobCategory { get; set; }
        public string JobRequirements { get; set; } = string.Empty;
        public int? HumanResourceId { get; set; }
        public List<Application>? Application { get; set; }
        public HumanResources? HumanResource { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
