using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class Interview
    {
        [Key]
        public int InterviewId { get; set; }

        public DateTime ScheduledDate { get; set; }
        public TimeSpan ScheduledTime { get; set; }
        public string? Mode { get; set; }
        public string? AssignTo { get; set; }
        public int? ApplicationId { get; set; }
        public int? HumanResourceId { get; set; }
        public Application? Application { get; set; }
        public HumanResources? HumanResource { get; set; }
       
    }
}
