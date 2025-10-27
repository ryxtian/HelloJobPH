﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class UserAccount
    {
        public int UserAccountId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsLoggedIn { get; set; } = false;
        public int userDetailsId { get; set; }
        public Applicant? Applicant { get; set; }
        public HumanResources? HumanResource { get; set; }
    }
}
