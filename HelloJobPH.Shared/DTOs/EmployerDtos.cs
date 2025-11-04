using HelloJobPH.Shared.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class EmployerDtos
    {
        public int EmployerId { get; set; }

        public string CompanyName { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;


        [EmailAddress]
        public string ContactEmail { get; set; } = string.Empty;

        [MaxLength(20)]
        public string ContactNumber { get; set; } = string.Empty;

        [Url]
        public string Website { get; set; } = string.Empty;
        public byte IsDeleted { get; set; } = 0;

        public int UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }
    }
}
