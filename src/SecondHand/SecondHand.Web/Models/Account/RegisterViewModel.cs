using SecondHand.Web.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SecondHand.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(Constraints.MAX_NAME_LEN, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Constraints.MIN_NAME_LEN)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(Constraints.MAX_PWD_LEN, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Constraints.MIN_PWD_LEN)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
