using AutoMapper;
using Bytes2you.Validation;
using SecondHand.Services.Data.Contracts;
using SecondHand.Services.Notifications.Contracts;
using SecondHand.Web.Areas.Chats.Models.Chats;
using SecondHand.Web.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecondHand.Web.Areas.Chats.Controllers
{
    [Authorize]
    [SaveChanges]
    public class ChatsController : Controller
    {
        private readonly IChatsService chatService;
        private readonly IMapper mapper;
        private readonly IChatNotificationsService chatNotificationService;

        public ChatsController(IChatsService chatService, IChatNotificationsService chatNotificationService, IMapper mapper)
        {
            Guard.WhenArgument(chatService, "chatService").IsNull().Throw();
            Guard.WhenArgument(mapper, "mapper").IsNull().Throw();
            Guard.WhenArgument(chatNotificationService, "chatNotificationService").IsNull().Throw();

            this.chatService = chatService;
            this.mapper = mapper;
            this.chatNotificationService = chatNotificationService;
        }

        public ActionResult Index()
        {
            var loggedUser = User.Identity.Name;

            var result = string.Format("Chats of user {0}", loggedUser);

            return this.Content(result);
        }

        [SaveChanges]
        public ActionResult ChatRoom(string username, Guid advertisementId)
        {
            var loggedUser = User.Identity.Name;
            var destinationUser = username;

            var participants = new string[] { loggedUser, destinationUser };

            var chat = this.chatService.GetChat(advertisementId, participants);

            if (chat == null)
            {
                return this.RedirectToAction("Details", "Advertisements", new { id = advertisementId.ToString(), area = "" });
            }

            var viewModel = this.mapper.Map<ChatRoomViewModel>(chat);

            // this.chatNotificationService.ClearChatNotification(chat, loggedUser);
            this.chatNotificationService.ClearChatNotification(chat, loggedUser);

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SaveChanges]
        public ActionResult AddMessage(CreateMessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Fix
                return this.Json(null);
            }

            var authorUsername = User.Identity.Name;

            var chat = this.chatService.GetChatById(model.ChatId);

            var chatParticipant = chat
                .Participants
                .FirstOrDefault(x => x.UserName.ToLower() == authorUsername);

            if (chatParticipant == null)
            {
                // Fix
                return this.Json(null);
            }

            var message = this.chatService.CreateMessage(chat, chatParticipant, model.Text);

            var viewModel = this.mapper.Map<MessageListItemViewModel>(message);

            // this.chatNotificationService.NotifyUsers(chat);
            this.chatNotificationService.NotifyUsers(chat, authorUsername);

            return this.PartialView("_ChatMessage", viewModel);
        }
    }
}