using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models.CrewViewModels
{
    public class CreateCrewViewModel
    {

        public CreateCrewViewModel()
        {

        }
       

        public string Name { get; set; }

        [DisplayName("Välj båt")]
        public Guid SelectedBoatId { get; set; }

        public IEnumerable<SelectListItem> BoatItems { get; set; }

        public List<ApplicationUser> Crewmen { get; set; }

    }
}
