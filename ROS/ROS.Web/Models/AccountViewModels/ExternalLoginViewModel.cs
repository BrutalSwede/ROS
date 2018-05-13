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

    }
}
