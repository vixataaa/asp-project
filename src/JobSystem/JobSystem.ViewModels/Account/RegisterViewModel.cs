using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JobSystem.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Registration type")]
        public string RegisterType { get; set; }

        public IEnumerable<SelectListItem> RegisterTypes
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Text = "Person", Value = "Person" },
                    new SelectListItem { Text = "Firm", Value = "Firm" }
                };
            }
        }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
