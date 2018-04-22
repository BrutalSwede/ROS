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

        public ApplicationUser Owner { get; set; }

        [DisplayName("Skapad")]
        public DateTime FoundedDate { get; set; }

        [Required]
        [DisplayName("Gick med")]
        public DateTime JoinedDate { get; set; }

        [Required]
        [DisplayName("Aktiv")]
        public bool IsActive { get; set; }

        public IList<ClubUser> ClubUsers { get; set; }
    }
}
