using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Whatsup_Her.Models
{
    public class Account
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage ="Account must have a name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Account must have a mobile number, you need to log in with this")]
        [Display(Name ="Mobile number")]
        public string MobileNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public Account(int id)
        {
            this.Id = id;
        }

        public Account() {  }
    }
}