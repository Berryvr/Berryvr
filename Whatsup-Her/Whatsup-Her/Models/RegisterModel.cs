using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Whatsup_Her.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Account must have a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Account must have a mobile number, you need to log in with this")]
        [Display(Name = "Mobile number")]
        public string MobileNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password", ErrorMessage ="The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
    }
}