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
    [Authorize(Roles = "Admin")]
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

        [HttpGet]
        public async Task<IActionResult> UserManagement()
        {
            var userList = new List<UserRolesViewModel>();

            var users = _userManager.Users;
            var roles = await _roleManager.FindByNameAsync("Admin");

            foreach (var item in users)
            {
                string temp;

                if(await _userManager.IsInRoleAsync(item, roles.Name))
                {
                    temp = "Admin"; 
                }
                else
                {
                    temp = "User";
                }

                userList.Add(new UserRolesViewModel
                {
                    User = item,
                    UserId = item.Id,
                    Role = temp
                });

                temp = null;
            }
            return View(userList);
        }


        //Post request that deletes the user if the user is found in db
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserManagement");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong with while deleting this user.");
                }
            }
            else
            {
                ModelState.AddModelError("", "This user could not be found");
            }

            return View("UserManagement", _userManager.Users);
        }

        //POST: Promote to Admin
        [HttpPost]
        public async Task<IActionResult> Promote(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            var result = await _userManager.AddToRoleAsync(user, "Admin");

            if (result.Succeeded == true)
            {
                return RedirectToAction("UserManagement");
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return RedirectToAction("UserManagement");
        }

        // GET all boats
        public async Task<IActionResult> BoatManagement()
        {
            var boats = await _context.Boats.Include(o => o.Owner).ToListAsync();

            return View(boats);
        }

        //Post request to delete boats
        [HttpPost]
        public async Task<IActionResult> DeleteBoat(Guid id)
        {
            var boat = await _context.Boats.FindAsync(id);

            if (boat != null)
            {
                _context.Boats.Remove(boat);
                await _context.SaveChangesAsync();
                return RedirectToAction("BoatManagement");

            }
            else
            {
                ModelState.AddModelError("", "The boat could not be found");
            }

            return View("BoatManagement", _context.Boats);
        }

        // GET all clubs
        public async Task<IActionResult> ClubManagement()
        {
            var clubs = await _context.Clubs.Include(o => o.Owner).ToListAsync();

            return View(clubs);
        }

        //Post request to delete clubs
        [HttpPost]
        public async Task<IActionResult> DeleteClub(Guid id)
        {
            var club = await _context.Clubs.FindAsync(id);

            if (club != null)
            {
                _context.Clubs.Remove(club);
                await _context.SaveChangesAsync();

                return RedirectToAction("ClubManagement");
            }
            else
            {
                ModelState.AddModelError("", "The club could not be found.");
            }

            return View("ClubManagement", _context.Clubs);
        }

        // GET all regattas
        public async Task<IActionResult> RegattaManagement()
        {
            var regattas = await _context.Regattas.Include(o => o.CreatedBy).ToListAsync();

            return View(regattas);
        }

        //Post request to delete regatta
        [HttpPost]
        public async Task<IActionResult> DeleteRegatta(Guid id)
        {
            var regatta = await _context.Regattas.FindAsync(id);

            if (regatta != null)
            {
                _context.Regattas.Remove(regatta);
                await _context.SaveChangesAsync();

                return RedirectToAction("RegattaManagement");
            }
            else
            {
                ModelState.AddModelError("", "The regatta could not be found.");
            }

            return View("RegattaManagement", _context.Regattas);
        }

    }
}
