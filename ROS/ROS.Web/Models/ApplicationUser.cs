using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ROS.Web.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        { }



        [Required]
        [DisplayName("Förnamn")]
        public string FirstName { get; set; }
        
        [Required]
        [DisplayName("Efternamn")]
        public string GivenName { get; set; }
        
        [Required]
        [Phone]
        [DisplayName("ICE Telefonnummer")]
        public string IcePhone { get; set; }

        public IList<Boat> Boats { get; set; }
    }
}
