using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class SetScheduleDto
    {
        public int ApplicationId { get; set; }
        public string Date { get; set; } = string.Empty;       // Could also be DateTime
        public string Time { get; set; } = string.Empty;       // Could also be TimeSpan
        public string? Location { get; set; }
        public int InterviewerId { get; set; }
        public string? InterviewBy { get; set; }
        public string Mode { get; set; } = string.Empty;
    }
    
}
