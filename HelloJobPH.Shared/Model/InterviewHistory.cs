using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class InterviewHistory
    {
        [Key]
        public int InterviewHistoryId { get; set; }                
        public int? ApplicationId { get; set; }
        public Application? Application { get; set; }
        public string CandidateName { get; set; } = string.Empty;
        public string Stage { get; set; } = string.Empty;   
        public string Status { get; set; } = string.Empty;
        public DateTime? ScheduledDate { get; set; }             
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



        public int? InterviewerId { get; set; } 
        public Interviewer? Interviewer { get; set; }
    }
}
