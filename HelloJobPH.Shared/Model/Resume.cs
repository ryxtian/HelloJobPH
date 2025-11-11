using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class Resume
    {
        [Key]
        public int ResumeId { get; set; }
        public string ResumeFileName { get; set; } = string.Empty;
        public byte[]? ResumeFileData { get; set; }
        public int? ApplicantId { get; set; }
        public byte IsDeleted { get; set; } = 0;
        public Applicant? Applicant { get; set; }
        //public byte IsActive { get; set; } = 0;
    }
}
