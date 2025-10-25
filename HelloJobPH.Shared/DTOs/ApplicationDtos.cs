using HelloJobPH.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class ApplicationListDtos
    {
        public int ApplicationId { get; set; }
        public DateTime DateApplied { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public string Firstname { get; set; }
        public string CompanyName { get; set; }
        public string Lastname { get; set; }
        public string Status { get; set; } = "Pending";

    }
}
