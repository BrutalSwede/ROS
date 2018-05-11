﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ROS.Web.Data;
using ROS.Web.Models;
using ROS.Web.Models.ClubViewModels;

namespace ROS.Web.Controllers
{
    [Authorize]
    public class ClubsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClubsController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Clubs
        public async Task<IActionResult> Index(string sortOrder)
        {
            var clubs = await _context.Clubs.Include(c => c.Owner)
                .Include(u => u.ClubUsers).Include(a => a.Applications).ToListAsync();

            var clubVMList = new List<GetClubsViewModel>();

            var currentUser = GetCurrentUser().Id;

            foreach (var item in clubs)
            {
                clubVMList.Add(new GetClubsViewModel
                {
                    ClubId = item.Id,
                    ClubName = item.Name,
                    FoundedDate = item.FoundedDate,
                    IsActive = item.IsActive,
                    NumberOfMembers = item.ClubUsers.Count,
                    Owner = item.Owner,
                    IsMember = item.ClubUsers.Exists(m => m.UserId == currentUser),
                    HasApplied = item.Applications.Exists(a => (a.UserId == currentUser && a.IsHandled == false))
                });
            }

            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["DateSortParm"] = sortOrder == "date" ? "date_desc" : "date";
            ViewData["MemberSortParm"] = sortOrder == "member" ? "member_desc" : "member";

            var sortQuery = from x in clubVMList
                            select x;

            switch (sortOrder)
            {
                case "name_desc":
                    sortQuery = sortQuery.OrderByDescending(s => s.ClubName);
                    break;

                case "name":
                    sortQuery = sortQuery.OrderBy(s => s.ClubName);
                    break;

                case "date":
                    sortQuery = sortQuery.OrderBy(s => s.FoundedDate);
                    break;

                case "date_desc":
                    sortQuery = sortQuery.OrderByDescending(s => s.FoundedDate);
                    break;

                case "member":
                    sortQuery = sortQuery.OrderBy(s => s.NumberOfMembers);
                    break;

                case "member_desc":
                    sortQuery = sortQuery.OrderByDescending(s => s.NumberOfMembers);
                    break;

                default:
                    break;
            }

            return View(sortQuery);
        }

        // GET: Clubs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _context.Clubs
                .Include(o => o.Owner)
                .Include(c => c.ClubUsers)
                .Include(a => a.Applications)
                .ThenInclude(k => k.User)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (club == null)
            {
                return NotFound();
            }

            return View(club);
        }

        [Authorize]
        // GET: MyClubs
        public async Task<IActionResult> MyClubs(string sortOrder)
        {
            string _userId = GetCurrentUser().Id;

            var clubs = await _context.Clubs.Where(c => c.Owner.Id == _userId)
                .Include(u => u.ClubUsers).ToListAsync();

            if (clubs.Count() <= 0)
            {
                return RedirectToAction(nameof(Create));
            }
            var clubVMList = new List<GetClubsViewModel>();

            foreach (var item in clubs)
            {
                clubVMList.Add(new GetClubsViewModel { ClubId = item.Id, ClubName = item.Name, FoundedDate = item.FoundedDate, IsActive = item.IsActive, NumberOfMembers = item.ClubUsers.Count, Owner = item.Owner });
            }

            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["DateSortParm"] = sortOrder == "date" ? "date_desc" : "date";
            ViewData["MemberSortParm"] = sortOrder == "member" ? "member_desc" : "member";

            var sortQuery = from x in clubVMList
                            select x;

            switch (sortOrder)
            {
                case "name_desc":
                    sortQuery = sortQuery.OrderByDescending(s => s.ClubName);
                    break;

                case "name":
                    sortQuery = sortQuery.OrderBy(s => s.ClubName);
                    break;

                case "date":
                    sortQuery = sortQuery.OrderBy(s => s.FoundedDate);
                    break;

                case "date_desc":
                    sortQuery = sortQuery.OrderByDescending(s => s.FoundedDate);
                    break;

                case "member":
                    sortQuery = sortQuery.OrderBy(s => s.NumberOfMembers);
                    break;

                case "member_desc":
                    sortQuery = sortQuery.OrderByDescending(s => s.NumberOfMembers);
                    break;

                default:
                    break;
            }

            return View(sortQuery);
        }

        // GET: Clubs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clubs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,FoundedDate,JoinedDate,IsActive,Id")] Club club)
        {
            if (ModelState.IsValid)
            {
                club.Owner = _context.Users.FirstOrDefault(u => u.UserName == HttpContext.User.Identity.Name);

                // Adds "Club Admin"-role to the user
                var user = _context.Users.FirstOrDefault(o => o.UserName == club.Owner.UserName);
                await _userManager.AddToRoleAsync(user, "Club Admin");

                club.JoinedDate = DateTime.Now;
                club.IsActive = true;
                club.Id = Guid.NewGuid();

                // adds user to club user table
                var clubUser = new ClubUser { User = user, ClubId = club.Id, Club = club, UserId = club.Owner.Id, Id = Guid.NewGuid() };
                _context.Add(clubUser);

                _context.Add(club);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(club);
        }

        // GET: Clubs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _context.Clubs.Include(o => o.Owner).SingleOrDefaultAsync(m => m.Id == id);
            if (club == null)
            {
                return NotFound();
            }

            string _userId = GetCurrentUser().Id;

            if (club.Owner.Id != _userId)
            {
                return Forbid();
            }

            return View(club);
        }

        // POST: Clubs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,FoundedDate,JoinedDate,IsActive,Id")] Club club)
        {
            if (id != club.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(club);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubExists(club.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(club);
        }

        // GET: Clubs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _context.Clubs.Include(o => o.Owner)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (club == null)
            {
                return NotFound();
            }

            string _userId = GetCurrentUser().Id;

            if (club.Owner.Id != _userId)
            {
                return Forbid();
            }

            return View(club);
        }

        // POST: Clubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var club = await _context.Clubs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Clubs.Remove(club);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Clubs/Apply/Id
        [HttpGet]
        public async Task<IActionResult> Apply(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _context.Clubs.SingleOrDefaultAsync(m => m.Id == id);

            if (club == null)
            {
                return NotFound();
            }

            return View(club);
        }

        //POST: Clubs/Apply/id
        [HttpPost, ActionName("Apply")]
        public async Task<IActionResult> ApplyConfirmed(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var club = await _context.Clubs.Include(a => a.Applications).SingleOrDefaultAsync(m => m.Id == id);

            var application = new ClubApplication
            {
                ClubId = club.Id,
                Date = DateTime.Now,
                UserId = GetCurrentUser().Id,
                IsHandled = false,
                Id = Guid.NewGuid(),
                Club = club,
                User = GetCurrentUser()
            };

            club.Applications.Add(application);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //GET: Clubs/Applications/id
        //[HttpGet]
        //public async Task<IActionResult> Applications(Guid id)
        //{
        //    if(id == null)
        //    {
        //        return NotFound();
        //    }

        //    var applications = await _context.Clubs.SingleOrDefaultAsync(o => o.Id == id);

        //    if(applications == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(applications);
        //}

        public async Task<IActionResult> ApproveApplication(Guid id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var application = _context.ClubApplications
                .Include(c => c.User)
                .Include(c => c.Club)
                .SingleOrDefaultAsync(c => c.Id == id);


            return View();
        }

        private bool ClubExists(Guid id)
        {
            return _context.Clubs.Any(e => e.Id == id);
        }


        #region Helpers
        public ApplicationUser GetCurrentUser()
        {
            return _context.Users.SingleOrDefault(u => u.UserName == HttpContext.User.Identity.Name);
        }
        #endregion

    }
}
