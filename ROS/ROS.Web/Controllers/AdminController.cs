using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ROS.Web.Data;
using ROS.Web.Models.AdminViewModels;

namespace ROS.Web.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET Boats
        public async Task<IActionResult> Index()
        {
            var boats = await _context.Boats.ToListAsync();

            var BoatVm = new AdminBoatViewModel { Boats = boats };

            return View(BoatVm);
        }
    }
}