using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class ChatDtos
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string? CompanyName { get; set; }
        public string Message { get; set; }
    }
}
