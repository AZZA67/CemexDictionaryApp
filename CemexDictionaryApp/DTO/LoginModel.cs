using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.DTO
{
    public class LoginModel
    {

        //[Required(ErrorMessage = "User Name is required")]
        //public string Username { get; set; }
        [Required(ErrorMessage = "Mobile Number is required")]
        public string Mobile_Number { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
