using SecondHand.Services.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondHand.Web.Models.Advertisements
{
    public class AdvertisementIndexViewModel
    {
        public IEnumerable<AdvertisementListItemViewModel> Advertisements { get; set; }

        public IEnumerable<string> SortableProperties
        {
            get
            {
                return new List<string>()
                {
                    "",
                    "Title",
                    "Price"
                };
            }
        }

        public IEnumerable<SecondHand.Services.Data.Common.SortType> SortTypes
        {
            get
            {
                return new List<SecondHand.Services.Data.Common.SortType>()
                {
                    SecondHand.Services.Data.Common.SortType.Ascending,
                    SecondHand.Services.Data.Common.SortType.Descending
                };
            }
        }

        public string SortType { get; set; }

        public string SortProperty { get; set; }

        public int PageCount { get; set; }
    }
}