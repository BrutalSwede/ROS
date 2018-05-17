using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models.BoatViewModels
{
    public class CreateBoatViewModel : IValidatableObject
    {
        [Required]
        [DisplayName("Båtnamn")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Båttyp")]
        public string Type { get; set; }

        [DisplayName("Segelnummer")]
        public int SailNumber { get; set; }

        [DisplayName("Modellår")]
        public int ModelYear { get; set; }

        [Url]
        [DisplayName("Certifikat")]
        public string Certificate { get; set; }

        [DisplayName("SRS")]
        public double? HandicapStandardWithForesail { get; set; }

        [Required]
        [DisplayName("SRS u. flygande segel")]
        public double? HandicapStandardWithoutForesail { get; set; }

        [DisplayName("SRS S/H")]
        public double? HandicapShorthandedWithForesail { get; set; }

        [Required]
        [DisplayName("SRS S/H u. flygande segel")]
        public double? HandicapShorthandedWithoutForesail { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(ModelYear > DateTime.Now.Year + 3 || ModelYear < 1600)
            {
                yield return new ValidationResult($"Modellår får vara mellan 1600 och {DateTime.Now.Year + 3}.", new[] { "ModelYear" });
            }

            if(SailNumber < 0 || SailNumber > 9999)
            {
                yield return new ValidationResult("Segelnummer får vara mellan 0 och 9999", new[] { "SailNumber" });
            }
        }
    }
}
