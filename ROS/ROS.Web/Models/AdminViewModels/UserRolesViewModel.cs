using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models.AdminViewModels
{
    public class UserRolesViewModel
    {
        public UserRolesViewModel()
        { }

        public string UserId { get; set; }
        public string Role { get; set; }
        public ApplicationUser User { get; set; }
    }
}
