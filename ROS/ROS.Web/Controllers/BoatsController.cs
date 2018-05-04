using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ROS.Web.Data;
using ROS.Web.Models;

namespace ROS.Web.Controllers
{
    [Authorize]
    public class BoatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Boats
        public async Task<IActionResult> Index()
        {
            string _userId = GetCurrentUser().Id;

            var boats = await _context.Boats.Where(o => o.Owner.Id == _userId).ToListAsync();

            return View(boats);
        }

        // GET: Boats/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // We want both the Captain and the Crewmembers from the crew.
            var boat = await _context.Boats
                .Include(b => b.Crews)              // It is possible to include properties on a collection:
                .ThenInclude(c => c.Captain)        // https://github.com/aspnet/EntityFrameworkCore/issues/6560
                .Include(b => b.Crews)
                .ThenInclude(c => c.Crewmen)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (boat == null)
            {
                return NotFound();
            }

            return View(boat);
        }

        // GET: Boats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Boats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Name,Type,Certificate,HandicapStandardWithForesail,HandicapStandardWithoutForesail,HandicapShorthandedWithForesail,HandicapShorthandedWithoutForesail,Id")] */Boat boat)
        {
            if (ModelState.IsValid)
            {
                boat.Owner = GetCurrentUser();
                boat.Id = Guid.NewGuid();
                _context.Add(boat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(boat);
        }

        // GET: Boats/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boat = await _context.Boats.Include(o => o.Owner).SingleOrDefaultAsync(m => m.Id == id);
            if (boat == null)
            {
                return NotFound();
            }

            string _userId = GetCurrentUser().Id;

            if (boat.Owner.Id != _userId)
            {
                return Forbid();
            }

            return View(boat);
        }

        // POST: Boats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Type,Certificate,HandicapStandardWithForesail,HandicapStandardWithoutForesail,HandicapShorthandedWithForesail,HandicapShorthandedWithoutForesail,Id")] Boat boat)
        {
            if (id != boat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoatExists(boat.Id))
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
            return View(boat);
        }

        // GET: Boats/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boat = await _context.Boats.Include(o => o.Owner)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (boat == null)
            {
                return NotFound();
            }

            string _userId = GetCurrentUser().Id;

            if (boat.Owner.Id != _userId)
            {
                return Forbid();
            }

            return View(boat);
        }

        // POST: Boats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var boat = await _context.Boats.SingleOrDefaultAsync(m => m.Id == id);
            _context.Boats.Remove(boat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoatExists(Guid id)
        {
            return _context.Boats.Any(e => e.Id == id);
        }

        #region Helpers
        
        public ApplicationUser GetCurrentUser()
        {
            return _context.Users.FirstOrDefault(u => u.UserName == HttpContext.User.Identity.Name);
        }

        #endregion
    }
}
