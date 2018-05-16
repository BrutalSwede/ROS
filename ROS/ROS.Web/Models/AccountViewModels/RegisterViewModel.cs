using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ROS.Web.Models.AccountViewModels
{
    public class RegisterViewModel
    {

        [DisplayName("Förnamn")]
        public string FirstName { get; set; }

        [DisplayName("Efternamn")]
        public string LastName { get; set; }

        [DisplayName("Adress")]
        public string Address { get; set; }

        [Phone]
        [DisplayName("Telefonnummer")]
        public string Phone { get; set; }

        [Phone]
        [DisplayName("ICE Telefonnummer")]
        public string IcePhone { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
