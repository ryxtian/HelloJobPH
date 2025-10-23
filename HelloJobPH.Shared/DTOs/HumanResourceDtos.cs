using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class HumanResourceDtos
    {
        public int HumanResourceId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte IsDeleted { get; set; } = 0;
        public string ProfilePhotoUrl { get; set; }
        public string JobTitle { get; set; }
    }
}
