using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models.ClubViewModels
{
    public class ClubDetailsViewModel
    {
        public Guid ClubId { get; set; }
        public string Name { get; set; }

        public ApplicationUser Owner { get; set; }

        public List<ClubUser> Members { get; set; }

        public DateTime FoundedDate { get; set; }
        public DateTime JoinedDate { get; set; }

        public bool IsActive { get; set; }

        public List<ClubApplication> PendingApplications { get; set; }


    }
}
