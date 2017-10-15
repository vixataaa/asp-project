using Microsoft.AspNet.SignalR;
using SecondHand.Services.Notifications.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Services.Notifications.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IChatNotificationsService serv;

        public NotificationHub(IChatNotificationsService serv)
        {
            this.serv = serv;
        }

        public void Hello(string str)
        {
            Clients.All.logMsg(str);
        }

        public override Task OnConnected()
        {
            Clients.All.updateNotifications(string.Format("You have {0} notifications", this.serv.UserNotificationsCount(Context.User.Identity.Name)));

            return base.OnConnected();
        }
    }
}
