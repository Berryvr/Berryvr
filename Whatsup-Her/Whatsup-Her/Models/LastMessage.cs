using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Whatsup_Her.Models
{
    public class LastMessage
    {
        public Contact Contact { get; set; }
        public Message Message { get; set; }
    }
}