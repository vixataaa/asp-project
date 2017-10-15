using Microsoft.AspNet.SignalR;
using Ninject.Extensions.Interception;
using SecondHand.Services.Notifications.Contracts;
using System.Threading.Tasks;
using System;
using SecondHand.Data.Models;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace SecondHand.Services.Notifications.Hubs
{
    public class NotificationHub : Hub
    {
        public NotificationHub()
        {
        }

        public void Hello(string str)
        {
            Clients.All.updateNotifications(str);
        }

        public override Task OnConnected()
        {
            Clients.Caller.requestNotifications();
            return base.OnConnected();
        }
    }
}
