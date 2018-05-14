using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Namn")]
        public ApplicationUser User { get; set; }

        public Guid BoatId { get; set; }
        [DisplayName("Båt")]
        public Boat Boat { get; set; }

        [DisplayName("Meddelande")]
        public string Message { get; set; }

        // Add crews later?
    }
}
