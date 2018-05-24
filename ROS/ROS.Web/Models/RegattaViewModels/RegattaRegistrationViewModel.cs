using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models.RegattaViewModels
{
    public class RegattaRegistrationViewModel : IValidatableObject
    {
        public Regatta Regatta { get; set; }

        [DisplayName("Välj båt")]
        [Required]
        public Guid SelectedBoatId { get; set; }

        public IEnumerable<SelectListItem> BoatItems { get; set; }

        [DisplayName("Meddelande")]
        public string Message { get; set; }

        [DisplayName("Antal deltagare")]
        public int NumberOfParticipants { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SelectedBoatId.Equals(Guid.Empty))
            {
                yield return new ValidationResult("Då måste välja en båt", new[] { "SelectedBoatId" });
            }
        }
    }
}
