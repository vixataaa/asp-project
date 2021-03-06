﻿using SecondHand.Data.Models;
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
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public string AdderUsername { get; set; }

        public string AddedById { get; set; }

        public string PrimaryImageUrl { get; set; }

        public decimal Price { get; set; }

        public CurrencyType CurrencyType { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Advertisement, AdvertisementListItemViewModel>()
                .ForMember(advVM => advVM.AdderUsername, cfg => cfg.MapFrom(x => x.AddedBy.UserName))
                .ForMember(advVM => advVM.PrimaryImageUrl, cfg => cfg.MapFrom(x => x.Photos.Count > 0 ? x.Photos.FirstOrDefault().Url : "http://via.placeholder.com/400x400?text=No+Image"))
                .ForMember(advVM => advVM.AddedById, cfg => cfg.MapFrom(x => x.AddedBy.Id));

        }
    }
}