using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class OverviewRequest
    {
        public IFormFile? ResumeFile { get; set; }
        public string? JobPostText { get; set; }
    }

    public class AIOverviewResponse
    {
        public string Overview { get; set; } = string.Empty;
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
