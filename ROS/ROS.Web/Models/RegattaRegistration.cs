using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models
{
    public class RegattaRegistration : BaseEntity
    {
        public RegattaRegistration()
        {

        }

        public Guid RegattaId { get; set; }
        public Regatta Regatta { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid BoatId { get; set; }
        public Boat Boat { get; set; }

        public string Message { get; set; }

        // Add crews later?
    }
}
