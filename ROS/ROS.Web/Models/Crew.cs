using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models
{
    public class Crew : BaseEntity
    {

        public Crew()
        {

        }

        [Required]
        public string Name { get; set; }

        [Required]
        public Boat Boat { get; set; }

        [Required]
        public Guid BoatId { get; set; }

        public ApplicationUser Captain { get; set; }

        public IList<CrewUser> Crewmen { get; set; }

    }
}
