using SecondHand.Data.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Data.Models
{
    public class Chat : DataModel
    {
        public virtual ICollection<ApplicationUser> Participants { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual Advertisement Advertisement { get; set; }

        public virtual ICollection<ChatNotification> Notifications { get; set; }
    }
}
