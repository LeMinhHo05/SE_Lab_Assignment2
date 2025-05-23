﻿using System.ComponentModel.DataAnnotations;

namespace Assignment2.Core.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; } // We won't implement this fully here, but include for structure
    }
}
