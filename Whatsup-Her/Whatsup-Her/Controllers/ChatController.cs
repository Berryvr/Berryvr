using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whatsup_Her.Repositories;
using Whatsup_Her.Models;

namespace Whatsup_Her.Controllers
{
    public class ChatController : Controller
    {
        ChatRepository repository = new ChatRepository();
        AccountRepository AccRepository = new AccountRepository();
        ContactRepository ContactRepository = new ContactRepository();
        MessageRepository MessageRepository = new MessageRepository();

        // GET: show list of contacts with the most recent message
        public ActionResult Index()
        {
            Account CurrentAcc = (Account)Session["loggedin_account"];
            List<Chat> Chats = repository.GetAllChats(CurrentAcc.Id);

            List<MessageView> messages = new List<MessageView>();


            //Show chats with the latest message.
            foreach (Chat chat in Chats)
            {
                Message LastMessage = MessageRepository.GetLastMessage(chat.ChatId);

                if (LastMessage != null)
                {
                    int chatId = LastMessage.ChatId;

                    string message = LastMessage.Text;

                    Contact contact = new Contact();
                    if (chat.FirstAccountId == CurrentAcc.Id)
                    {
                        contact = ContactRepository.GetWithOwner(CurrentAcc.Id, chat.SecondAccountId);
                    }
                    else { contact = ContactRepository.GetWithOwner(CurrentAcc.Id, chat.FirstAccountId); }

                    DateTime timeSent = LastMessage.TimeSent;
                    if (contact != null)
                    {
                        messages.Add(new MessageView(chatId, message, contact, timeSent, contact.ContactAccountId));
                    }
                }
            }

            return View(messages);
        }

        // Here you can send someone messages and read previous messages
        public ActionResult Message(int? OtherAccountId)
        {
            Account CurrentAcc = (Account)Session["loggedin_account"];

            // Test if messagereceiver is correct.
            if (OtherAccountId == null || OtherAccountId == CurrentAcc.Id) { return RedirectToAction("Index"); }

            int CurrentChatId = repository.GetChatId(CurrentAcc, OtherAccountId);
            // If Chat doesn't exist between accounts, create a new chat.
            if (CurrentChatId == 0)
            {
                Account OtherAccount = AccRepository.GetAccountById(OtherAccountId);
                repository.CreateChat(new Chat(CurrentAcc, OtherAccount));
                return RedirectToAction("Message", "Chat", new { otheraccountid = OtherAccountId });
            }

            Session["CurrentChat"] = CurrentChatId;

            return View();
        }
        
        // Partial that returns view with messages in corresponding chat
        public ActionResult Messages()
        {
            List<Message> messages = null;

            int CurrentChatId = (int)Session["CurrentChat"];

            messages = repository.GetMessages(CurrentChatId);
            return PartialView(ChangeToMessageView(messages));
        }

        public List<MessageView> ChangeToMessageView(List<Message> messages)
        {
            Account CurrentAcc = (Account)Session["loggedin_account"];
            List<MessageView> MessageViews = new List<MessageView>();

            foreach (Message message in messages)
            {
                MessageView view = new MessageView();
                view.Message = message.Text;
                if (message.SenderAccountId == CurrentAcc.Id)
                {
                    view.ContactName = "You";
                }
                else
                {
                    view.ContactName = ContactRepository.GetWithOwner(CurrentAcc.Id, message.SenderAccountId).Name;
                }
                view.TimeSent = String.Format("{0}:{1} {2}/{3}/{4}", message.TimeSent.Hour, message.TimeSent.Minute, message.TimeSent.Day, message.TimeSent.Month, message.TimeSent.Year);
                MessageViews.Add(view);
            }

            return MessageViews;
        }

        // Called when someone sends a message
        [HttpPost]
        public ActionResult ChatBox(Message message)
        {
            Account CurrentAcc = (Account)Session["loggedin_account"];
            message.SenderAccountId = CurrentAcc.Id;

            int CurrentChatId = (int)Session["CurrentChat"];
            message.ChatId = CurrentChatId;
            message.TimeSent = DateTime.Now;

            MessageRepository.Send(message);

            return PartialView();
        }
        
        // returns partial with a textbox to send messages
        public ActionResult ChatBox()
        {
            return PartialView();
        }
    }
}