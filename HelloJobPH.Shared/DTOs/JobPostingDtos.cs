using HelloJobPH.Shared.Enums;
using HelloJobPH.Shared.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class JobPostingDtos
    {
        public int JobPostingId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public EmploymentType EmploymentType { get; set; } 
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
        public JobCategory JobCategory { get; set; }
        public string JobRequirements { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public byte IsDeleted { get; set; } = 0;
        public byte IsActive { get; set; } = 1;
        public int HumanResourceId { get; set; }
        public HumanResources? HumanResource { get; set; }
        public Employers? Employers { get; set; }
        public DateTime PostedDate { get; set; }

        public int? EmployerId { get; set; }
    }
    public class JobPostingListDtos
    {
        public int JobPostingId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public EmploymentType EmploymentType { get; set; } // e.g., Full-Time, Part-Time, Contract
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
        public JobCategory JobCategory { get; set; }
        public string JobRequirements { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public byte IsDeleted { get; set; } = 0;
        public int HumanResourceId { get; set; }
        //public int HumanResourceId { get; set; } = 0;
        public HumanResources? HumanResource { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? ExpiredDate { get; set; }

    }
 }
