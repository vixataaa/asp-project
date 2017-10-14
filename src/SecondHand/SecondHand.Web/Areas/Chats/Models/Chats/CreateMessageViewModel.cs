using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecondHand.Web.Areas.Chats.Models.Chats
{
    public class CreateMessageViewModel
    {
        [Required]
        public Guid ChatId { get; set; }

        public string Text { get; set; }
    }
}