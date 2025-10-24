using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class HumanResourceDtos
    {
        public int HumanResourceId { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string PhoneNumber { get; set; }
        public byte IsDeleted { get; set; } = 0;
        public string ProfilePhotoUrl { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string JobTitle { get; set; }
        public string? Password { get; set; }
    }
}
