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

        //All ClubsOwners
        [HttpGet("clubowners")]
        public IEnumerable<ClubOwnerApiViewModel> Clubowners()
        {
            var clubs = _context.Clubs.Include(o => o.ClubUsers).Include(s=> s.Owner);

            if (clubs == null)
                return null;

            var clubOwnerApiViewModel = new List<ClubOwnerApiViewModel>();

            foreach (var item in clubs)
                clubOwnerApiViewModel.Add(new ClubOwnerApiViewModel { ClubName = item.Name, FirstName = item.Owner.FirstName, LastName = item.Owner.LastName, OwnerEmail = item.Owner.Email, IsActive = item.IsActive });

            return clubOwnerApiViewModel.OrderByDescending(o => o.ClubName);
        } 

        private bool ClubExists(Guid id)
        {
            return _context.Clubs.Any(e => e.Id == id);
        }
    }
}