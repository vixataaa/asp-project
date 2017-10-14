using AutoMapper;
using Bytes2you.Validation;
using SecondHand.Services.Data.Contracts;
using SecondHand.Web.Areas.Chats.Models.Chats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecondHand.Web.Areas.Chats.Controllers
{
    [Authorize]
    public class ChatsController : Controller
    {
        private readonly IChatsService chatService;
        private readonly IMapper mapper;

        public ChatsController(IChatsService chatService, IMapper mapper)
        {
            Guard.WhenArgument(chatService, "chatService").IsNull().Throw();
            Guard.WhenArgument(mapper, "mapper").IsNull().Throw();

            this.chatService = chatService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var loggedUser = User.Identity.Name;

            var result = string.Format("Chats of user {0}", loggedUser);

            return this.Content(result);
        }

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

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

            // this.chatNotificationService.NotifyUsers(chat.Participants);

            return this.PartialView("_ChatMessage", viewModel);
        }
    }
}