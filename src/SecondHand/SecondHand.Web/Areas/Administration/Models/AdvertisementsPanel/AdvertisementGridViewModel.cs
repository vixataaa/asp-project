using SecondHand.Data.Models;
using SecondHand.Web.Common.Constants;
using SecondHand.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecondHand.Web.Areas.Administration.Models.AdvertisementsPanel
{
    public class AdvertisementGridViewModel : IMapFrom<Advertisement>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(Constraints.MAX_TITLE_LEN, MinimumLength = Constraints.MIN_TITLE_LEN)]
        public string Title { get; set; }

        [Required]
        [StringLength(Constraints.MAX_DESCRIPTION_LEN, MinimumLength = Constraints.MIN_DESCRIPTION_LEN)]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}