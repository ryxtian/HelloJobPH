using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class PasswordResetToken
    {
        public int Id { get; set; }

        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public string? Email { get; set; }
    }

}
