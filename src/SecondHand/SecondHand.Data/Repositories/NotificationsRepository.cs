using SecondHand.Data.Models;
using SecondHand.Data.Repositories.Base;
using SecondHand.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Data.Repositories
{
    public class NotificationsRepository : EfRepository<ChatNotification>, INotificationsRepository
    {
        public NotificationsRepository(MsSqlDbContext context) 
            : base(context)
        {
        }
    }
}
