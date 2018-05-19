using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Models.ApiEndpoints
{
    public class BoatApiViewModel
    {

        public int NumberOfBoats { get; set; }

        public string BoatName { get; set; }

        public string BoatType { get; set; }

        public DateTime BoatYear { get; set; }

    }

}
