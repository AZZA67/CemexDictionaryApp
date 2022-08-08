using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.DTO
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]

        public string Email { get; set; }

        [Required(ErrorMessage = "National id is required ")]
        [Display(Name = "National ID")]
        public string NationalID{ get; set; }
        [Required(ErrorMessage = "Address is required ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Mobile Number  id is required ")]
        [Display(Name = "Mobile Number")]
        public string Mobileno { get; set; }

        [Required(ErrorMessage = "Password is required")]
       
        public string Password { get; set; }

      
      



    }
}
