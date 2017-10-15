using SecondHand.Data.Models;
using SecondHand.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using SecondHand.Web.Common.Constants;

namespace SecondHand.Web.Models.Advertisements
{
    public class AdvertisementEditViewModel : IMapFrom<Advertisement>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(Constraints.MAX_TITLE_LEN, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Constraints.MIN_TITLE_LEN)]
        public string Title { get; set; }

        [Required]
        [StringLength(Constraints.MAX_DESCRIPTION_LEN, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Constraints.MIN_DESCRIPTION_LEN)]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public CurrencyType CurrencyType { get; set; }
    }
}