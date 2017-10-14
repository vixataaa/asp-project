using SecondHand.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Services.Notifications.Contracts
{
    public interface IChatNotificationsService
    {
        void ClearChatNotification(Chat chat, string username);

        void NotifyUsers(IEnumerable<ApplicationUser> participants);
    }
}
