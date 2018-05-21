using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models
{
    public class RegattaRegistration : BaseEntity, IValidatableObject
    {
        public RegattaRegistration()
        {

        }

        public Guid RegattaId { get; set; }
        public Regatta Regatta { get; set; }

        public string UserId { get; set; }
        [DisplayName("Namn")]
        public ApplicationUser User { get; set; }

        public Guid BoatId { get; set; }
        [DisplayName("Båt")]
        public Boat Boat { get; set; }

        [DisplayName("Meddelande")]
        public string Message { get; set; }

        [Required]
        [DisplayName("Antal deltagare")]
        public int NumberOfParticipants { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(NumberOfParticipants < 1)
            {
                yield return new ValidationResult("Antal deltagare kan ej vara mindre än 1", new[] { "NumberOfParticipants" });
            }

            if(BoatId.Equals(Guid.Empty))
            {
                yield return new ValidationResult("Då måste välja en båt", new[] { "BoatId" });
            }
        }
    }
}
