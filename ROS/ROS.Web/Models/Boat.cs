using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models
{
    public class Boat : BaseEntity
    {
        public Boat()
        { }
        
        public ApplicationUser Owner { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Url]
        public string Certificate { get; set; }

        [DisplayName("SRS")]
        public double? HandicapStandardWithForesail { get; set; }

        [Required]
        [DisplayName("SRS u. flygande segel")]
        public double? HandicapStandardWithoutForesail { get; set; }
        
        [DisplayName("SRS S/H")]
        public double? HandicapShorthandedWithForesail { get; set; }

        [Required]
        [DisplayName("SRS S/H u. flygande segel")]
        public double? HandicapShorthandedWithoutForesail { get; set; }
    }
}
