using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Services.Notifications.Hubs
{
    public class NotificationHub : Hub
    {
        public void Hello(string str)
        {
            Clients.All.logMsg(str);
        }
    }
}
