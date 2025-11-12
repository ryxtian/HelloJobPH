using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class JobpostAuditDtos
    {
        public int Id { get; set; }
        public int JobpostId { get; set; }

        public string Title { get; set; }

        public string Action { get; set; } = ""; // e.g., "Created", "Updated", "Deleted"

        public string ChangedBy { get; set; } = "";

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    }
}
