using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models.ApiEndpoints
{
    public class ClubApiViewModel
    {
        public ClubApiViewModel()
        { }

        public string ClubName { get; set; }

        public int NumberOfMembers { get; set; }

    }
}
