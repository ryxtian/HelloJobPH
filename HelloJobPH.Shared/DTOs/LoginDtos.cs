using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class LoginDtos
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Password { get; set; }
      string? Token { get; set; }
    }
    public class TokenResponse
    {
        public string Token { get; set; } = "";
    }
}
