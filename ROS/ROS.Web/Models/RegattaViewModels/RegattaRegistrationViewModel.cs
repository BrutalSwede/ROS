using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models.RegattaViewModels
{
    public class RegattaRegistrationViewModel
    {
        public Regatta Regatta { get; set; }

        [DisplayName("Välj båt")]
        [Required]
        public Guid SelectedBoatId { get; set; }

        public IEnumerable<SelectListItem> BoatItems { get; set; }

        [DisplayName("Meddelande")]
        public string Message { get; set; }

    }
}
