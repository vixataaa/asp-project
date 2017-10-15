using SecondHand.Data.Models;
using SecondHand.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace SecondHand.Web.Areas.Chats.Models.Chats
{
    public class ChatListItemViewModel : IMapFrom<Chat>, IHaveCustomMappings
    {
        public Guid AdvertisementId { get; set; }

        public string AdvertisementTitle { get; set; }

        public string AdvertisementAddedByUserName { get; set; }

        public ICollection<string> NotifiedUsers { get; set; }

        public bool IsRead { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Chat, ChatListItemViewModel>()
                .ForMember(vm => vm.NotifiedUsers, ch => ch.MapFrom(dataModel => dataModel.Notifications.Where(n => !n.IsDeleted).Select(ntf => ntf.User.UserName)));
        }
    }
}