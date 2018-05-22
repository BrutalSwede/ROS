using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models.RegattaViewModels
{
    public class CreateRegattaViewModel
    {
        [Required]
        [MaxLength(50)]
        [DisplayName("Titel")]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        [DisplayName("Beskrivning")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Starttid")]
        public DateTime StartTime { get; set; }

        [Required]
        [DisplayName("Sluttid")]
        public DateTime EndTime { get; set; }

        [Required]
        [DisplayName("Adress")]
        public string Address { get; set; }

        [DisplayName("Värd")]
        public ApplicationUser CreatedBy { get; set; }

        [DisplayName("Värdklubb")]
        public Guid HostingClubId { get; set; }

        public IEnumerable<SelectListItem> HostingClubs { get; set; }
    }
}
