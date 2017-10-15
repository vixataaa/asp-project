using SecondHand.Data.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Data.Models
{
    public class ChatNotification : DataModel
    {
        public virtual Chat Chat { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
