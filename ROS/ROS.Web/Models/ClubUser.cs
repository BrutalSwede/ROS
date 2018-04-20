using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models
{
    public class ClubUser : BaseEntity
    {
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public Club Club { get; set; }
        public Guid ClubId { get; set; }
    }
}
