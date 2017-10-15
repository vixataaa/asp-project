using SecondHand.Web.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Web.Models.Manage
{
    public class ChangeUserDetailsViewModel
    {
        [Display(Name = "First name")]
        [StringLength(Constraints.MAX_NAME_LEN, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Constraints.MIN_NAME_LEN)]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [StringLength(Constraints.MAX_NAME_LEN, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Constraints.MIN_NAME_LEN)]
        public string LastName { get; set; }
    }
}
