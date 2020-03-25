using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Whatsup_Her.Models
{
    public class LoginModel
    {
        public LoginModel()
        {

        }
        public LoginModel(string pnumber, string pass)
        {
            this.MobileNumber = pnumber;
            this.Password = pass;
        }

        public string MobileNumber { get; set; }
        public string Password { get; set; }
    }
}