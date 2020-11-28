using Bytes2you.Validation;
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
    public class ChatsRepository : EfRepository<Chat>, IChatsRepository
    {
        public ChatsRepository(MsSqlDbContext context) : base(context)
        {
        }

        public Chat FindChat(Guid advertisementId, params string[] participantNames)
        {
            var result = this.All;

            if (participantNames.Length < 2)
            {
                return null;
            }

            result = result.Where(x => x.Advertisement.Id == advertisementId);

            foreach (var name in participantNames)
            {
                result = result.Where(x => x.Participants.Any(p => p.UserName.ToLower() == name.ToLower()));
            }

            return result.FirstOrDefault();
        }

        public Chat GetById(Guid id)
        {
            return this.All.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Chat> GetUserChats(string username)
        {
            var result = this
                .All
                .Where(x => x.Participants.Any(p => p.UserName.ToLower() == username.ToLower()) &&
                    x.Advertisement.IsDeleted == false);

            return result;
        }
    }
}
