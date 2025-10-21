using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class JobPostingDtos
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string EmploymentType { get; set; } // e.g., Full-Time, Part-Time, Contract
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
        public string JobRequirements { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
