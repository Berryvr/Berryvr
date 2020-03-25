using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Whatsup_Her.Models;

namespace Whatsup_Her.Repositories
{
    public class MessageRepository
    {
        WhatsUpContext db = new WhatsUpContext();

        public void Send(Message message)
        {
            db.Messages.Add(message);
            db.SaveChanges();
        }

        public Message GetLastMessage(int id)
        {
            List<Message> messages = db.Messages.Where(m => m.ChatId == id).ToList();
            messages.OrderByDescending(m => m.TimeSent.Ticks);

            try { return messages[messages.Count - 1]; }
            catch { return null; }
        }
    }
}