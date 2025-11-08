using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class InterviewHistory
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; } // Link to Application
        public Application? Application { get; set; }
        public string Stage { get; set; } // e.g., "Technical Interview", "Phone Screening"
        public string Status { get; set; } // e.g., "Scheduled", "Completed", "In Progress", "Under Review"
        public string Interviewer { get; set; } // e.g., "Emily Rodriguez"
        public DateTime? ScheduledDate { get; set; } // Nullable if not scheduled
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
