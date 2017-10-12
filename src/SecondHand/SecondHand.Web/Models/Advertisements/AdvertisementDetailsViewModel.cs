using SecondHand.Data.Models;
using SecondHand.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper;

namespace SecondHand.Web.Models.Advertisements
{
    public class AdvertisementDetailsViewModel : IMapFrom<Advertisement>, IHaveCustomMappings
    { 
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Photo1 { get; set; }

        public string Photo2 { get; set; }

        public string Photo3 { get; set; }

        public string Category { get; set; }

        public string CurrencyType { get; set; }

        public string PublisherName { get; set; }

        public string PublisherId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Advertisement, AdvertisementDetailsViewModel>()
                .ForMember(advVM => advVM.Category, cfg => cfg.MapFrom(adv => adv.Category.Name))
                .ForMember(advVM => advVM.CurrencyType, cfg => cfg.MapFrom(adv => adv.CurrencyType))
                .ForMember(advVM => advVM.Photo1, cfg => cfg.MapFrom(adv => adv.Photos.Count >= 1 ? adv.Photos.ElementAt(0).Url : ""))
                .ForMember(advVM => advVM.Photo2, cfg => cfg.MapFrom(adv => adv.Photos.Count >= 2 ? adv.Photos.ElementAt(1).Url : ""))
                .ForMember(advVM => advVM.Photo3, cfg => cfg.MapFrom(adv => adv.Photos.Count >= 3 ? adv.Photos.ElementAt(2).Url : ""))
                .ForMember(advVM => advVM.PublisherName, cfg => cfg.MapFrom(adv => adv.AddedBy.UserName))
                .ForMember(advVM => advVM.PublisherId, cfg => cfg.MapFrom(adv => adv.AddedBy.Id));
        }
    }
}