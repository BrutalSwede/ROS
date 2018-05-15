using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DisplayName("Förnamn")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Efternamn")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Adress")]
        public string Address { get; set; }

        [Required]
        [Phone]
        [DisplayName("Telefonnummer")]
        public string PhoneNumber { get; set; }

        [Required]
        [Phone]
        [DisplayName("ICE Telefonnummer")]
        public string IcePhone { get; set; }

    }
}
