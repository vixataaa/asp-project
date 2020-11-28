using SecondHand.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondHand.Data.Models;
using SecondHand.Data.Repositories.Contracts;
using SecondHand.Data.Contracts;
using Bytes2you.Validation;

namespace SecondHand.Services.Data
{
    public class ChatsService : IChatsService
    {
        private readonly IChatsRepository chats;
        private readonly IAdvertisementsRepository advertisements;
        private readonly IUsersRepository users;

        public ChatsService(IChatsRepository chats, IAdvertisementsRepository advertisements,
            IUsersRepository users)
        {
            Guard.WhenArgument(chats, "chats").IsNull().Throw();
            Guard.WhenArgument(advertisements, "advertisements").IsNull().Throw();
            Guard.WhenArgument(users, "users").IsNull().Throw();

            this.chats = chats;
            this.advertisements = advertisements;
            this.users = users;
        }

        public Chat CreateChat(Guid advertisementId, params string[] participantNames)
        {
            var foundChat = this.chats.FindChat(advertisementId, participantNames);

            if (foundChat != null)
            {
                return null;
            }

            var advertisement = this.advertisements.GetById(advertisementId);

            if (advertisement == null)
            {
                return null;
            }

            var participants = new List<ApplicationUser>();

            foreach (var name in participantNames)
            {
                var foundUser = this.users.GetByUsername(name);

                if (foundUser == null)
                {
                    return null;
                }

                participants.Add(foundUser);
            }

            var chat = new Chat
            {
                Advertisement = advertisement,
                Participants = participants,
                Messages = new List<Message>()
            };

            this.chats.Add(chat);
            return chat;
        }

        public Chat GetChat(Guid advertisementId, params string[] participantNames)
        {
            var chat = this.chats.FindChat(advertisementId, participantNames);

            if (chat == null)
            {
                return this.CreateChat(advertisementId, participantNames);
            }

            return this.chats.FindChat(advertisementId, participantNames);
        }

        public Chat GetChatById(Guid id)
        {
            return this.chats.GetById(id);
        }

        public Message CreateMessage(Chat chat, ApplicationUser author, string text)
        {
            var message = new Message
            {
                Author = author,
                Text = text
            };

            chat.Messages.Add(message);
            this.chats.Update(chat);

            return message;
        }

        public IEnumerable<Chat> GetUserChats(string username)
        {
            return this.chats.GetUserChats(username).ToList();
        }
    }
}
