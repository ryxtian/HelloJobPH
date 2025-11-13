using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class ChatDtos
    {
        public int Id { get; set; }
        public string SenderId { get; set; } = default!;
        public string ReceiverId { get; set; } = default!;
        public string Message { get; set; } = default!;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
            
        public string? CompanyName { get; set; }
    }
}
