using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ROS.Web.Data;
using ROS.Web.Models;
using ROS.Web.Models.AdminViewModels;

namespace ROS.Web.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET all users
        public IActionResult UserManagement()
        {
            var users = _userManager.Users;

            return View(users);
        }

        // GET all boats
        public async Task<IActionResult> BoatManagement()
        {
            var boats = await _context.Boats.Include(o => o.Owner).ToListAsync();
            
            return View(boats);
        }
       
        // GET all clubs
        public async Task<IActionResult> ClubManagement()
        {
            var clubs = await _context.Clubs.Include(o => o.Owner).ToListAsync();

            return View(clubs);
        }

        // GET all regattas
        public async Task<IActionResult> RegattaManagement()
        {
            var regattas = await _context.Regattas.Include(o => o.CreatedBy).ToListAsync();

            return View(regattas);
        }
        

        //TODO: Edit and Delete User, Boat, Club, Regatta


    }
}