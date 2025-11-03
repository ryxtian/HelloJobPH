using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Enums
{
    public enum EmploymentType
    {
        [Display(Name = "Full Time")]
        FullTime,
        [Display(Name ="Part Time")]
        PartTime,
        [Display(Name = "Contract")]
        Contract,
        [Display(Name = "Remote")]
        Remote
    }
}
