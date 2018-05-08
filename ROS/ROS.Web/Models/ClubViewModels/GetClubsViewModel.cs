using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models.ClubViewModels
{
    public class GetClubsViewModel
    {
        public GetClubsViewModel()
        {
        }

        [DisplayName("Klubbnamn")]
        public string ClubName { get; set; }

        public Guid ClubId { get; set; }

        [DisplayName("Antal medlemmar")]
        public int NumberOfMembers { get; set; }

        [DisplayName("Aktiv")]
        public bool IsActive { get; set; }

        [DisplayName("Skapad")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime FoundedDate { get; set; }

        [DisplayName("Ägare")]
        public ApplicationUser Owner { get; set; }
    }
}
