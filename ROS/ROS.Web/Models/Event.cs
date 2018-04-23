using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models
{
    public class Event : BaseEntity, IValidatableObject
    {
        public Event()
        {

        }

        [Required]
        [DisplayName("Titel")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [DisplayName("Beskrivning")]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Eventtyp")]
        public EventType EventType { get; set; }

        [Required]
        [DisplayName("Startdatum")]
        public DateTime StartTime { get; set; }

        [Required]
        [DisplayName("Slutdatum")]
        public DateTime EndTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(EndTime <= StartTime)
            {
                yield return new ValidationResult(errorMessage: "Slutdatum måste vara senare än startdatum.", 
                    memberNames: new[] { "EndTime" });
            }
        }
    }

    public enum EventType
    {
        [Description("Socialt")]
        Social,
        [Description("Race")]
        Race
    }
}
