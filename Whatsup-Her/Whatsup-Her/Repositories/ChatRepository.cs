using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Whatsup_Her.Models;
using System.Data.Entity;

namespace Whatsup_Her.Repositories
{
    public class ChatRepository
    {
        WhatsUpContext db = new WhatsUpContext();

        public int GetChatId(Account currentAcc, int? otherAccountId)
        {
            Chat chat = db.Chats.FirstOrDefault(c => c.FirstAccountId == currentAcc.Id && c.SecondAccountId == otherAccountId);

            if (chat == null)
            {
                chat = db.Chats.FirstOrDefault(c => c.FirstAccountId == otherAccountId && c.SecondAccountId == currentAcc.Id);
            }

            if (chat == null)
            {
                return 0;
            }

            return chat.ChatId;
        }

        internal List<Chat> GetAllChats(int AccountId)
        {
            IEnumerable<Chat> chats = db.Chats.Where(c => c.FirstAccountId == AccountId);
            IEnumerable<Chat> chats2 = db.Chats.Where(c => c.SecondAccountId == AccountId);

            List<Chat> AllChats = new List<Chat>();

            foreach (Chat chat in chats)
            {
                AllChats.Add(chat);
            }

            foreach (Chat chat in chats2)
            {
                AllChats.Add(chat);
            }

            return AllChats;
        }

        public void CreateChat(Chat chat)
        {
            db.Chats.Add(chat);
            db.SaveChanges();
        }

        internal List<Message> GetMessages(int chatId)
        {
            return db.Messages.Where(m => m.ChatId == chatId).OrderByDescending(m => m.TimeSent).ToList();
        }

        internal Chat GetChatById(int chatId)
        {
            return db.Chats.Find(chatId);
        }
    }
}