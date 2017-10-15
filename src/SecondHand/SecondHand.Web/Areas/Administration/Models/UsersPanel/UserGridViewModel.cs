using SecondHand.Data.Models;
using SecondHand.Web.Common.Constants;
using SecondHand.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecondHand.Web.Areas.Administration.Models.UsersPanel
{
    public class UserGridViewModel : IMapFrom<ApplicationUser>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(Constraints.MAX_NAME_LEN, MinimumLength = Constraints.MIN_NAME_LEN)]
        public string UserName { get; set; }

        [StringLength(Constraints.MAX_NAME_LEN, MinimumLength = Constraints.MIN_NAME_LEN)]
        public string FirstName { get; set; }

        [StringLength(Constraints.MAX_NAME_LEN, MinimumLength = Constraints.MIN_NAME_LEN)]
        public string LastName { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }
    }
}