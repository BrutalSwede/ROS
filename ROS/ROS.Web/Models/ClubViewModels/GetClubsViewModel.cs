using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models.ClubViewModels
{
    public class GetClubsViewModel
    {
        public GetClubsViewModel()
        {
        }

        public string ClubName { get; set; }

        public Guid ClubId { get; set; }

        [DisplayName("Antal Medlemmar")]
        public int NumberOfMembers { get; set; }

        public bool IsActive { get; set; }

        public DateTime FoundedDate { get; set; }

        public ApplicationUser Owner { get; set; }
    }
}
