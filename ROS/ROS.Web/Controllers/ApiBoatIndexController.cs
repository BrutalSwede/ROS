using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ROS.Web.Data;
using ROS.Web.Models;
using ROS.Web.Models.ApiEndpoints;

namespace ROS.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/ApiBoatIndex")]
    public class ApiBoatIndexController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApiBoatIndexController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        
        //Tommy Api function, Gets the most basic information about the registered boats 
        // GET: api/ApiBoatIndex/basicboatinfo
        [HttpGet("{basicboatinfo}")]
        public IEnumerable<BoatApiViewModel> BasicBoatInfo()
        {
            var boats = _context.Boats;

            if (boats == null)
            {
                return null;
            }


            var boatApiViewModelList = new List<BoatApiViewModel>();


            foreach (var item in boats)
            {
                BoatApiViewModel boatApiViewModel = new BoatApiViewModel();


                boatApiViewModel.BoatName = item.Name;
                boatApiViewModel.BoatType = item.Type;
                boatApiViewModel.BoatYear = item.ModelYear;

                boatApiViewModelList.Add(boatApiViewModel);
                
            }

            return boatApiViewModelList.OrderBy(o => o.BoatName);
            
        }

        
        
        private bool BoatExists(Guid id)
        {
            return _context.Boats.Any(e => e.Id == id);
        }
    }
}