using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models.ClubViewModels
{
    public class ClubDetailsViewModel
    {
        public Guid ClubId { get; set; }

        [DisplayName("Klubbnamn")]
        public string Name { get; set; }

        [DisplayName("Ägare")]
        public ApplicationUser Owner { get; set; }

        [DisplayName("Medlemmar")]
        public List<ClubUser> Members { get; set; }

        [DisplayName("Antal medlemmar")]
        public int NumberOfMembers { get; set; }

        [DisplayName("Grundad")]
        public DateTime FoundedDate { get; set; }

        [DisplayName("Gick med")]
        public DateTime JoinedDate { get; set; }

        [DisplayName("Aktiv")]
        public bool IsActive { get; set; }
        
        public List<ClubApplication> PendingApplications { get; set; }


    }
}
