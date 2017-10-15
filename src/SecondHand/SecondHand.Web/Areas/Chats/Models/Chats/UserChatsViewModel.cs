using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondHand.Web.Areas.Chats.Models.Chats
{
    public class UserChatsViewModel
    {
        public IEnumerable<ChatListItemViewModel> Chats { get; set; }
    }
}