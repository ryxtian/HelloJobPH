using HelloJobPH.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class EmployerListDtos
    {
        public int EmployerId { get; set; }

        public string CompanyName { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
