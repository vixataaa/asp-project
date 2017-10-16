using AutoMapper;
using Bytes2you.Validation;
using SecondHand.Services.Data.Contracts;
using SecondHand.Services.Notifications.Contracts;
using SecondHand.Web.Areas.Chats.Models.Chats;
using SecondHand.Web.Infrastructure;
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
        private readonly IUsersService userService;

        public ChatsController(IChatsService chatService, IChatNotificationsService chatNotificationService, IUsersService userService, IMapper mapper)
        {
            Guard.WhenArgument(chatService, "chatService").IsNull().Throw();
            Guard.WhenArgument(mapper, "mapper").IsNull().Throw();
            Guard.WhenArgument(chatNotificationService, "chatNotificationService").IsNull().Throw();
            Guard.WhenArgument(userService, "userService").IsNull().Throw();

            this.chatService = chatService;
            this.mapper = mapper;
            this.chatNotificationService = chatNotificationService;
            this.userService = userService;
        }

        public ActionResult Index()
        {
            var loggedUser = this.ControllerContext.HttpContext.User.Identity.Name;

            var chats = this
                .chatService
                .GetUserChats(loggedUser)
                .Select(x => this.mapper.Map<ChatListItemViewModel>(x))
                .ToList();

            foreach (var chat in chats)
            {
                if (chat.NotifiedUsers.Any(x => x.ToLower() == loggedUser.ToLower()))
                {
                    chat.IsRead = false;
                }
                else
                {
                    chat.IsRead = true;
                }
            }

            var viewModel = new UserChatsViewModel
            {
                Chats = chats
            };

            return this.View(viewModel);
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
            
            this.chatNotificationService.NotifyUsers(chat, authorUsername, message.Text);

            return this.PartialView("_ChatMessage", viewModel);
        }

        [HttpPost]
        [SaveChanges]
        public ActionResult RemoveNotification(Guid chatId)
        {
            var loggedUser = User.Identity.Name;
            var chat = this.chatService.GetChatById(chatId);

            if (chat != null && chat.Participants.Any(x => x.UserName.ToLower() == loggedUser))
            {
                this.chatNotificationService.ClearChatNotification(chat, loggedUser);
                return this.Json("Notification removed");
            }

            return new EmptyResult();
        }

        [HttpPost]
        [SaveChanges]
        public ActionResult GetNotificationsCount()
        {
            var loggedUser = User.Identity.Name;

            var count = this
                .userService
                .GetByUsername(loggedUser)
                .Notifications
                .Count(x => !x.IsDeleted);

            return this.Json(count);
        }
    }
}