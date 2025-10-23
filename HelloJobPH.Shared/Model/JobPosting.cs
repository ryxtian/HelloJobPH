﻿using HelloJobPH.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class JobPosting
    {
        public int JobPostingId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public EmploymentType EmploymentType { get; set; } // e.g., Full-Time, Part-Time, Contract
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
        public byte IsDeleted { get; set; } = 0;
        public string JobRequirements { get; set; }
        public int HumanResourceId { get; set; }  // foreign key
        public HumanResource HumanResource { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
