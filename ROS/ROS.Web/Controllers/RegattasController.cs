using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ROS.Web.Data;
using ROS.Web.Models;
using ROS.Web.Models.RegattaViewModels;

namespace ROS.Web.Controllers
{
    [Authorize]
    public class RegattasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegattasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Regattas
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Regattas
                .Where(d => d.EndTime > DateTime.Now)
                .Include(r => r.CreatedBy)
                .ToListAsync());
        }

        // GET: Past Regattas
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> PastRegattas()
        {
            return View(await _context.Regattas
                .Where(d => d.EndTime < DateTime.Now)
                .Include(r => r.CreatedBy)
                .ToListAsync());
        }

        // GET: Regattas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regatta = await _context.Regattas
                .Include(r => r.CreatedBy)
                .Include(r => r.Registrations)
                .Include(r => r.HostingClub)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (regatta == null)
            {
                return NotFound();
            }

            // Check if the user is already registered
            ViewData["IsRegistered"] = regatta.Registrations.Exists(element => element.UserId == GetCurrentUser().Id);
            ViewData["NavUrlEncoded"] = HttpUtility.UrlEncode(regatta.Address);


            return View(regatta);
        }

        public async Task<IActionResult> ViewRegistrations(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regatta = await _context.Regattas
                .Include(r => r.CreatedBy)
                .SingleOrDefaultAsync(r => r.Id == id);

            ViewData["RegattaTitle"] = regatta.Title;

            if (HttpContext.User.Identity.Name != regatta.CreatedBy.Email)
            {
                return Forbid();
            }

            var registrations = await _context.RegattaRegistration
                .Include(r => r.User)
                .Include(r => r.Boat)
                .Where(r => r.RegattaId == id).ToListAsync();

            ViewData["TotalParticipants"] = registrations.Sum(r => r.NumberOfParticipants);


            if (registrations == null)
            {
                return NotFound();
            }

            return View(registrations);
        }

        // GET: Regattas/Create
        public async Task<IActionResult> Create()
        {

            var userId = GetCurrentUser().Id;

            var clubsSelectListItems = await _context.Clubs
                .Include(c => c.Owner)
                .Where(c => c.Owner.Id == userId)
                .Select(club => new SelectListItem { Value = club.Id.ToString(), Text = club.Name })
                .ToListAsync();

            if(clubsSelectListItems.Count == 0)
            {
                return RedirectToAction("Create", "Clubs");
            }

            clubsSelectListItems.Insert(0, new SelectListItem { Value = "", Text = "Välj värdklubb" });

            var createRegattaViewModel = new CreateRegattaViewModel
            {
                HostingClubs = new SelectList(clubsSelectListItems, "Value", "Text"),
                StartTime = DateTime.Today,
                EndTime = DateTime.Today.AddDays(1)
            };

            return View(createRegattaViewModel);
        }

        // POST: Regattas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,HostingClubId,Description,StartTime,EndTime,Address,Id")] CreateRegattaViewModel regatta)
        {
            if (ModelState.IsValid)
            {
                var _currentUser = GetCurrentUser();
                var hostClub = await _context.Clubs.SingleOrDefaultAsync(c => c.Id.Equals(regatta.HostingClubId));

                var newRegatta = new Regatta
                {
                    Id = Guid.NewGuid(),
                    CreatedBy = _currentUser,
                    HostingClub = hostClub,
                    Title = regatta.Title,
                    Address = regatta.Address,
                    Description = regatta.Description,
                    StartTime = regatta.StartTime,
                    EndTime = regatta.EndTime
                };

                _context.Add(newRegatta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(regatta);
        }

        // GET: Regattas/Register/{guid}
        public async Task<IActionResult> Register(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regatta = await _context.Regattas.SingleOrDefaultAsync(r => r.Id == id);

            if (regatta == null)
            {
                return NotFound();
            }

            // Get the boats from the user and convert them to SelectListItems to be consumed by the view.
            var _boatItems = _context.Boats.Where(b => b.Owner.Id == GetCurrentUser().Id)
                .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList();

            // Insert default value.
            _boatItems.Insert(0, new SelectListItem { Value = "", Text = "Välj båt" });

            var regattaVm = new RegattaRegistrationViewModel { Regatta = regatta, BoatItems = new SelectList(_boatItems, "Value", "Text") };

            return View(regattaVm);
        }

        [HttpPost, ActionName("Register")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RegisterConfirmed(Guid? id, RegattaRegistrationViewModel registration)
        {

            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Regatta regatta = await _context.Regattas
                    .Include(r => r.Registrations)
                    .SingleOrDefaultAsync(r => r.Id == id);
                var _user = GetCurrentUser();

                // Check if there is already a registration for this user
                if (regatta.Registrations.Exists(r => r.UserId == _user.Id))
                {
                    // If you have already registered you should be redirected somewhere else.
                    return RedirectToAction(nameof(Index));
                }

                // Get the boat the user selected from the dropdown
                Boat _boat = await _context.Boats.SingleOrDefaultAsync(b => b.Id == registration.SelectedBoatId);

                var newRegistration = new RegattaRegistration
                {
                    Regatta = regatta,
                    Boat = _boat,
                    User = _user,
                    Message = registration.Message,
                    NumberOfParticipants = registration.NumberOfParticipants
                };

                _context.RegattaRegistration.Add(newRegistration);
                _context.SaveChanges();
            }

            // Create Registration confirmation view!
            return RedirectToAction(nameof(Index));
        }


        // GET: Regattas/Edit/{guid}
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regatta = await _context.Regattas
                .Include(r => r.CreatedBy)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (regatta == null)
            {
                return NotFound();
            }

            string _userId = GetCurrentUser().Id;

            if (regatta.CreatedBy.Id != _userId)
            {
                return Forbid();
            }

            return View(regatta);
        }

        // POST: Regattas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Description,StartTime,EndTime,Address,Id")] Regatta regatta)
        {
            if (id != regatta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(regatta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegattaExists(regatta.Id))
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
            return View(regatta);
        }

        // GET: Regattas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regatta = await _context.Regattas
                .Include(r => r.CreatedBy)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (regatta == null)
            {
                return NotFound();
            }

            if (regatta.CreatedBy.Id != GetCurrentUser().Id)
            {
                return Forbid();
            }

            return View(regatta);
        }

        // POST: Regattas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var regatta = await _context.Regattas.SingleOrDefaultAsync(m => m.Id == id);
            _context.Regattas.Remove(regatta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegattaExists(Guid id)
        {
            return _context.Regattas.Any(e => e.Id == id);
        }

        #region Helpers

        public ApplicationUser GetCurrentUser()
        {
            return _context.Users.SingleOrDefault(u => u.UserName == HttpContext.User.Identity.Name);
        }

        #endregion
    }
}
