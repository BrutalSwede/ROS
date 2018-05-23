using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ROS.Web.Models.ManageViewModels
{
    public class ProfileViewModel
    {
        [DisplayName("Förnamn")]
        public string FirstName { get; set; }

        [DisplayName("Efternamn")]
        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public int AmountOfBoats { get; set; }

        [Phone]
        [DisplayName("Telefonnummer")]
        public string Phone { get; set; }

        [Phone]
        [DisplayName("ICE Telefonnummer")]
        public string IcePhone { get; set; }

        public IList<ClubApplication> Applications { get; set; }
        public IList<Boat> Boats { get; set; }
    }
}
