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
        private readonly ISaveContext context;
        private readonly IChatsRepository chats;
        private readonly IAdvertisementsRepository advertisements;
        private readonly IUsersRepository users;

        public ChatsService(IChatsRepository chats, IAdvertisementsRepository advertisements, 
            IUsersRepository users, ISaveContext context)
        {
            Guard.WhenArgument(chats, "chats").IsNull().Throw();
            Guard.WhenArgument(advertisements, "advertisements").IsNull().Throw();
            Guard.WhenArgument(users, "users").IsNull().Throw();
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.chats = chats;
            this.advertisements = advertisements;
            this.users = users;
            this.context = context;
        }

        public void CreateChat(Guid advertisementId, params string[] participantNames)
        {
            var foundChat = this.chats.FindChat(advertisementId, participantNames);

            if (foundChat != null)
            {
                return;
            }

            var advertisement = this.advertisements.GetById(advertisementId);

            if (advertisement == null)
            {
                return;
            }

            var participants = new List<ApplicationUser>();

            foreach (var name in participantNames)
            {
                var foundUser = this.users.GetByUsername(name);

                if (foundUser == null)
                {
                    return;
                }

                participants.Add(foundUser);
            }

            var chat = new Chat
            {
                Advertisement = advertisement,
                Participants = participants,
            };

            this.chats.Add(chat);
        }

        public Chat GetChat(Guid advertisementId, params string[] participantNames)
        {
            var chat = this.chats.FindChat(advertisementId, participantNames);

            // Questionable
            if (chat == null)
            {
                this.CreateChat(advertisementId, participantNames);
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

            return message;
        }
    }
}
