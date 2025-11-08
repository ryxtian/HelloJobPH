using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class AuditLogDtos
    {
        public int AuditLogId { get; set; }
        public int ApplicationId { get; set; }
        public string CandidateFirstName { get; set; } = string.Empty;
        public string CandidateLastName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string HRPersonnelName { get; set; } = string.Empty;
        public string HRDepartment { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
