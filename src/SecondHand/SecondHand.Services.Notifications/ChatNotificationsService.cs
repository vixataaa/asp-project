using SecondHand.Services.Notifications.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondHand.Data.Models;
using Microsoft.AspNet.SignalR;
using SecondHand.Services.Notifications.Hubs;
using SecondHand.Data.Repositories.Contracts;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace SecondHand.Services.Notifications
{
    public class ChatNotificationsService : IChatNotificationsService
    {
        private readonly INotificationsRepository notifications;
        private readonly IHubContext notificationContext;
        private readonly IUsersRepository users;

        public ChatNotificationsService(INotificationsRepository notifications, IUsersRepository users, IHubContext notificationContext)
        {
            this.notificationContext = notificationContext;
            this.notifications = notifications;
            this.users = users;
        }

        public int UserNotificationsCount(string username)
        {
            return this.users.GetByUsername(username).Notifications.Where(x => x.IsDeleted == false).Count();
        }

        public void ClearChatNotification(Chat chat, string username)
        {
            var toRemove = this.notifications.All
                .FirstOrDefault(x => x.User.UserName.ToLower() == username.ToLower()
                    && x.Chat.Id == chat.Id);

            if (toRemove != null)
            {
                this.notifications.Delete(toRemove);
            }
        }

        public void NotifyUsers(Chat chat, string excludedUser)
        {
            var notifHub = GlobalHost.DependencyResolver.Resolve<IConnectionManager>().GetHubContext<NotificationHub>();

            foreach (var participant in chat.Participants)
            {
                if (participant.UserName.ToLower() != excludedUser.ToLower())
                {
                    if (!participant.Notifications.Any(x => x.Chat.Id == chat.Id && x.IsDeleted == false))
                    {
                        var notification = new ChatNotification
                        {
                            Chat = chat,
                            User = participant
                        };

                        this.notifications.Add(notification);
                    }

                    notifHub
                        .Clients
                        .User(participant.UserName)
                        .filterNotifications();

                    notifHub
                        .Clients
                        .User(participant.UserName)
                        .updateNotifications(participant.Notifications.Count(x => !x.IsDeleted));
                }
            }
        }
    }
}
