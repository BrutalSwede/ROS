using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models
{
    public class Club : BaseEntity
    {
        public Club()
        { }

        [Required]
        [DisplayName("Namn")]
        public string Name { get; set; }

        [DisplayName("Ägare")]
        public ApplicationUser Owner { get; set; }

        [DisplayName("Skapad")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime FoundedDate { get; set; }

        [Required]
        [DisplayName("Gick med")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime JoinedDate { get; set; }

        [Required]
        [DisplayName("Aktiv")]
        public bool IsActive { get; set; }

        public List<ClubApplication> Applications { get; set; }

        public List<ClubUser> ClubUsers { get; set; }
    }
}
