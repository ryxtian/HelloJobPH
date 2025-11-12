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
        public string SenderId { get; set; } = default!;
        public string ReceiverId { get; set; } = default!;
        public string Message { get; set; } = default!;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        // Navigation properties
       
    }
}
