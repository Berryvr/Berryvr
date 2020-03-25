using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Whatsup_Her.Models
{
    public class MessageView
    {
        public int ChatId { get; set; }
        public string Message { get; set; }
        public string ContactName { get; set; }
        public string TimeSent { get; set; }
        public int OtherAccount { get; set; }

        public MessageView(int id, string message, Contact contact, DateTime TimeSent, int otherAccount)
        {
            this.ChatId = id;
            this.Message = message;
            this.ContactName = contact.Name;
            this.TimeSent = String.Format("{0}:{1:00} {2}/{3}/{4}", TimeSent.Hour, TimeSent.Minute, TimeSent.Day, TimeSent.Month, TimeSent.Year);
            this.OtherAccount = otherAccount;
        }

        public MessageView() { }
    }
}