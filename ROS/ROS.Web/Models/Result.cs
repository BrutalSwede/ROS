using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models
{
    public class Result : BaseEntity
    {
        public Result()
        {

        }

        // public Event Event { get; set; } // These will be needed later
        // public Entry Entry { get; set; }

        [Required]
        public double Distance { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        public double CalculatedDistance { get; set; } // Calculated from the boats handicap

        [Required]
        public TimeSpan CalculatedTime { get; set; } // Calculated from the boats handicap

    }
}
