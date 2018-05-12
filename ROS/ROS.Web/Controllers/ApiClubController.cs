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
    [Route("api/ApiClub")]
    public class ApiClubController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApiClubController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiClub
        [HttpGet]
        public IEnumerable<Club> GetClubs()
        {
            return _context.Clubs;
        }

        // GET: api/ApiClub/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClub([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var club = await _context.Clubs.SingleOrDefaultAsync(m => m.Id == id);

            if (club == null)
            {
                return NotFound();
            }

            return Ok(club);
        }


        //Jockes Api function. Gets the top 5 clubs sorted by most members 
        //GET: api/ApiClub/top5mostmembers
        [HttpGet("top5mostmembers")]
        public IEnumerable<ClubApiViewModel> Top5MostMembers()
        {
            var clubs = _context.Clubs.Include(o => o.ClubUsers);

            if(clubs == null)
            {
                return null;
            }

            var clubApiViewModel = new List<ClubApiViewModel>();

            foreach(var item in clubs)
            {
                clubApiViewModel.Add(new ClubApiViewModel { ClubName = item.Name, NumberOfMembers = item.ClubUsers.Count() });
            }
            
            return clubApiViewModel.OrderByDescending(o => o.NumberOfMembers).Take(5);
        }


        private bool ClubExists(Guid id)
        {
            return _context.Clubs.Any(e => e.Id == id);
        }
    }
}