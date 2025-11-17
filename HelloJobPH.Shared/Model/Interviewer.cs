using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class Interviewer
    {
        [Key]
        public int InterviewerId { get; set; }
        public string? Name { get; set; }
        public List<Interview>? Interviews { get; set; }
    }
}
