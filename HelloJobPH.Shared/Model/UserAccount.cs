﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class UserAccount
    {
        public int UserAccountId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
        public bool IsLoggedIn { get; set; } = false;
        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }
    }
}
