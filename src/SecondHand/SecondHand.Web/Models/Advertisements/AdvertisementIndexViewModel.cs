using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondHand.Web.Models.Advertisements
{
    public class AdvertisementIndexViewModel
    {
        public IEnumerable<AdvertisementListItemViewModel> Advertisements { get; set; }
    }
}