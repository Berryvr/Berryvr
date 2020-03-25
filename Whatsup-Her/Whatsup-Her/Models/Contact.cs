using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Whatsup_Her.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Mobile number")]
        public string MobileNumber { get; set; }

        [Required]
        public string Name { get; set; }
        
        public int OwnerAccountId { get; set; }
        public int ContactAccountId { get; set; }
    }
}