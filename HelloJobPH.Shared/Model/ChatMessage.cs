using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string? SenderId { get; set; } = string.Empty;      // user id or employer id
        public string? ReceiverId { get; set; } = string.Empty;    // other party
        public string? CompanyName { get; set; } = string.Empty;
        public string? Message { get; set; } = string.Empty;
        public DateTime? SentAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
    }
}
