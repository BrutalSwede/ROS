using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROS.Web.Data;
using ROS.Web.Models;
using ROS.Web.Models.LandingPageViewModels;

namespace ROS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(LandingPage));
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult LandingPage()
        {
            ApplicationUser applicationUser = _context.ApplicationUser.SingleOrDefault(m => m.Email == HttpContext.User.Identity.Name);
            IList<Club> clubList = new List<Club>();
            LandingPageViewModel landingPageView = new LandingPageViewModel { FirstName = applicationUser.FirstName, LastName = applicationUser.LastName, Applications = new List<ClubApplication>() };
            foreach (Club club in _context.Clubs)
            {
                if (club.Owner != null && club.Owner == applicationUser)
                {
                    clubList.Add(club);
                }

            }

            foreach (ClubApplication clubAppl in _context.ClubApplications)
            {
                foreach (Club C in clubList)
                {
                    if (clubAppl.ClubId == C.Id)
                    {
                        landingPageView.Applications.Add(clubAppl);
                    }
                }
            }
            return View(landingPageView);
        }
    }
}
