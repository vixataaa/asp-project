using System.Web.Mvc;

namespace SecondHand.Web.Areas.Chats
{
    public class ChatsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Chats";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Chatroom",
                "Chats/{username}/{advertisementId}",
                new { action = "ChatRoom", controller = "Chats", },
                new { advertisementId = @"^[{(]?[0-9A-F]{8}[-]?([0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$" }
            );

            context.MapRoute(
                "User chats",
                "Chats",
                new { action = "Index", controller = "Chats" }
            );

            context.MapRoute(
                "default area",
                "Chats/{controller}/{action}",
                new { action = "Index", controller = "Chats" }
            );
        }
    }
}