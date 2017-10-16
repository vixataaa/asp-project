using SecondHand.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Services.Data.Contracts
{
    public interface IChatsService
    {
        Chat GetChat(Guid advertisementId, params string[] participantNames);

        Chat GetChatById(Guid id);

        Chat CreateChat(Guid advertisementId, params string[] participantNames);

        Message CreateMessage(Chat chat, ApplicationUser author, string text);

        IEnumerable<Chat> GetUserChats(string username);
    }
}
