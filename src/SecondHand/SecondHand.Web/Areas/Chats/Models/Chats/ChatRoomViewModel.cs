using SecondHand.Data.Models;
using SecondHand.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace SecondHand.Web.Areas.Chats.Models.Chats
{
    public class ChatRoomViewModel : IMapFrom<Chat>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public string AdvertisementTitle { get; set; }

        public IEnumerable<MessageListItemViewModel> Messages { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Chat, ChatRoomViewModel>()
                .ForMember(vM => vM.Messages, ch => ch.MapFrom(x => x.Messages.OrderBy(m => m.CreatedOn)));
        }
    }
}