using System;

namespace ROS.Web.Models
{
    public class CrewUser : BaseEntity
    {
        public CrewUser()
        {

        }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public Crew Crew { get; set; }
        public Guid CrewId { get; set; }
    }
}