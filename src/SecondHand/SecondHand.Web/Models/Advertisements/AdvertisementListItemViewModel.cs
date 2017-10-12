using SecondHand.Data.Models;
using SecondHand.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace SecondHand.Web.Models.Advertisements
{
    public class AdvertisementListItemViewModel : IMapFrom<Advertisement>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public string AdderUsername { get; set; }

        public string AddedById { get; set; }


        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Advertisement, AdvertisementListItemViewModel>()
                .ForMember(advVM => advVM.AdderUsername, cfg => cfg.MapFrom(x => x.AddedBy.UserName));
        }
    }
}