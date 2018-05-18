using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models.ApiEndpoints
{
    public class ClubOwnerApiViewModel
    {
       
        [DisplayName("Klubbnamn")]
        public string ClubName { get; set; }

        [DisplayName("Förnamn")]
        public string FirstName { get; set; }

        [DisplayName("Efternamn")]
        public string LastName { get; set; }

        [DisplayName("ÄgareEmail")]
        public string OwnerEmail { get; set; }

        [DisplayName("Aktiv")]
        public bool IsActive { get; set; }

    }
}
