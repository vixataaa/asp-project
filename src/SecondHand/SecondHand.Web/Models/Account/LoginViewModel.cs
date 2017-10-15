using SecondHand.Web.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Web.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        [StringLength(Constraints.MAX_NAME_LEN, MinimumLength = Constraints.MIN_NAME_LEN)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
