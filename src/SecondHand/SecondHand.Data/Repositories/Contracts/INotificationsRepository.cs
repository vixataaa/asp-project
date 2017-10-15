using SecondHand.Data.Contracts;
using SecondHand.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Data.Repositories.Contracts
{
    public interface INotificationsRepository : IEfRepository<ChatNotification>
    {
    }
}
