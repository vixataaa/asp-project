using SecondHand.Services.Notifications.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondHand.Data.Models;
using Microsoft.AspNet.SignalR;
using SecondHand.Services.Notifications.Hubs;

namespace SecondHand.Services.Notifications
{
    public class ChatNotificationsService : IChatNotificationsService
    {
        private readonly IHubContext<NotificationHub> notificationContext;

        public ChatNotificationsService(IHubContext<NotificationHub> notificationContext)
        {
            this.notificationContext = notificationContext;
        }

        public void ClearChatNotification(Chat chat, string username)
        {
            throw new NotImplementedException();
        }

        public void NotifyUsers(IEnumerable<ApplicationUser> participants)
        {
            throw new NotImplementedException();
        }
    }
}
