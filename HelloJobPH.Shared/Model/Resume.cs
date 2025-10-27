using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class Resume
    {
        public int ResumeId { get; set; }
        public string ResumeUrl { get; set; }
        public int? ApplicantId { get; set; }
        public Applicant? Applicant { get; set; }
    }
}
