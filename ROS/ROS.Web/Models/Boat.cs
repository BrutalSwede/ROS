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

        public IList<Crew> Crews { get; set; }

        [Required]
        [DisplayName("Båtnamn")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Båttyp")]
        public string Type { get; set; }

        [DisplayName("Segelnummer")]
        public int SailNumber { get; set; }

        [DisplayName("Modellår")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public DateTime ModelYear { get; set; }


        [Url]
        [DisplayName("Certifikat")]
        public string Certificate { get; set; }

        [DisplayName("Standard")]
        public double? HandicapStandardWithForesail { get; set; }

        [Required]
        [DisplayName("Standard u. flygande segel")]
        public double? HandicapStandardWithoutForesail { get; set; }
        
        [DisplayName("Shorthand")]
        public double? HandicapShorthandedWithForesail { get; set; }

        [Required]
        [DisplayName("Shorthand u. flygande segel")]
        public double? HandicapShorthandedWithoutForesail { get; set; }
    }
}
