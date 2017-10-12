using SecondHand.Data.Models;
using SecondHand.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Web.Mvc;

namespace SecondHand.Web.Models.Advertisements
{
    public class AdvertisementCreationViewModel : IMapFrom<Advertisement>, IHaveCustomMappings
    {
        public IEnumerable<SelectListItem> Categories { get; set; }

        public IEnumerable<SelectListItem> CurrencyTypes = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "BGN", Value = "BGN" },
            new SelectListItem() { Text = "EUR", Value = "EUR" },
            new SelectListItem() { Text = "USD", Value = "USD" }
        };

        [Required]
        [Display(Name = "Title")]
        [StringLength(80, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Photo1 { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Photo2 { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Photo3 { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Currency type")]
        public string CurrencyType { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Advertisement, AdvertisementCreationViewModel>()
                .ForMember(advVM => advVM.Category, cfg => cfg.MapFrom(adv => adv.Category.Name))
                .ForMember(advVM => advVM.CurrencyType, cfg => cfg.MapFrom(adv => adv.CurrencyType))
                .ForMember(advVM => advVM.Photo1, cfg => cfg.MapFrom(adv => adv.Photos.Count >= 1 ? adv.Photos.ElementAt(0).Url : ""))
                .ForMember(advVM => advVM.Photo2, cfg => cfg.MapFrom(adv => adv.Photos.Count >= 2 ? adv.Photos.ElementAt(1).Url : ""))
                .ForMember(advVM => advVM.Photo3, cfg => cfg.MapFrom(adv => adv.Photos.Count >= 3 ? adv.Photos.ElementAt(2).Url : ""));

            

            configuration.CreateMap<AdvertisementCreationViewModel, Advertisement>()
                .ForMember(adv => adv.Category, cfg => cfg.MapFrom(x => new Category { Name = x.Category }))
                .ForMember(adv => adv.Photos, cfg => cfg.MapFrom(x => MapperUtils.GeneratePhotosList(x.Photo1, x.Photo2, x.Photo3)))
                .ForMember(adv => adv.CurrencyType, cfg => cfg.MapFrom(x => Enum.Parse(typeof(CurrencyType), x.CurrencyType, true)));
        }

        
    }
}
