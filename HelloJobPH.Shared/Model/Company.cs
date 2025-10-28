using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;

      
        public string TIN { get; set; } = string.Empty; // Tax Identification Number (BIR)
        public string SECNumber { get; set; } = string.Empty; // SEC Registration Number
        public string DTIRegistrationNumber { get; set; } = string.Empty; // DTI Number
        public string BusinessPermitNumber { get; set; } = string.Empty;

        public DateTime DateRegistered { get; set; }
        public bool IsActive { get; set; } = true;


        //public ICollection<CompanyCertificate> Certificates { get; set; } = new List<CompanyCertificate>();
        //public ICollection<BusinessRequirement> BusinessRequirements { get; set; } = new List<BusinessRequirement>();

    }
}
