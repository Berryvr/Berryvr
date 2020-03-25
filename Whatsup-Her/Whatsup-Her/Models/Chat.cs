using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Whatsup_Her.Models
{
    [Table("Chats")]
    public class Chat
    {
        [Key]
        public int ChatId { get; set; }

        public int FirstAccountId { get; set; }

        public int SecondAccountId { get; set; }

        public Chat(Account currentAcc, Account otherAccount)
        {
            this.FirstAccountId = currentAcc.Id;

            this.SecondAccountId = otherAccount.Id;
        }

        public Chat() { }
    }
}