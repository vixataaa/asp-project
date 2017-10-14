using SecondHand.Data.Models;
using SecondHand.Web.Infrastructure;
using System;

namespace SecondHand.Web.Areas.Chats.Models.Chats
{
    public class MessageListItemViewModel : IMapFrom<Message>
    {
        public string Text { get; set; }

        public string AuthorUserName { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}