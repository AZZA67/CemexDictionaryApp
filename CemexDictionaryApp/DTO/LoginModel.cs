﻿using System.ComponentModel.DataAnnotations;

namespace CemexDictionaryApp.DTO
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mobile Number is required")]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^(011|012|010|015)[0-9]{8}",ErrorMessage = "Phone is not valid")]
        public string Mobileno { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
