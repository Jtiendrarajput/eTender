using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eTender.Models
{
    public class _ChangePassword
    {
        public string oldpassword { get; set; }
        public string newpassword { get; set; }
        public string confirmPassword { get; set; } 
    }
}