using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models
{
    public class Regatta : BaseEntity
    {
        public Regatta()
        {

        }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        /* 
         * This can be a street address or a coordinate. 
         * We'll use this with the Google Maps api or Open Street Maps
        */
        [Required]
        public string Address { get; set; }

        public ApplicationUser CreatedBy { get; set; }

        //public List<Event> Events { get; set; }

        //public IList<Entry> Entries { get; set; }
    }
}
