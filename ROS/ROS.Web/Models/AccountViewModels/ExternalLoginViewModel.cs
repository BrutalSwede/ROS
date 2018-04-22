using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        
        [Required]
        [Display(Name = "F�rnamn")]
        public string SureName { get; set; }

        [Required]
        [Display(Name = "Efternamn")]
        public string AfterName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-Postadrss")]
        public string Email { get; set; }
    }
}
