using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class Applicant
    {
        [Key]
        public int ApplicantId { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public int? HumanResourceId { get; set; }
        public HumanResources? HumanResources { get; set; }
        public int UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }
    }
}
