using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models
{
    public class ClubApplication : BaseEntity
    {
        public ClubApplication()
        { }

        [Required]
        public Guid ClubId { get; set; }

        [Required]
        public Club Club { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool IsHandled { get; set; }
        
    }
}
