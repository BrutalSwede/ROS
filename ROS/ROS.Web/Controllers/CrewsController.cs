using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ROS.Web.Data;
using ROS.Web.Models;
using ROS.Web.Models.CrewViewModels;

namespace ROS.Web.Controllers
{
    [Authorize]
    public class CrewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CrewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Crews
        public async Task<IActionResult> Index()
        {
            return View(await _context.Crews.ToListAsync());
        }

        // GET: Crews/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crew = await _context.Crews
                .Include(c => c.Captain)
                .Include(c => c.Boat)
                .Include(c => c.Crewmen)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (crew == null)
            {
                return NotFound();
            }

            return View(crew);
        }

        // GET: Crews/Create
        public IActionResult Create()
        {
            var boatItems = _context.Boats.Where(b => b.Owner.UserName == HttpContext.User.Identity.Name)
                .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList();

            CreateCrewViewModel crewVM = new CreateCrewViewModel();

            crewVM.BoatItems = new SelectList(boatItems, "Value", "Text");
           
            return View(crewVM);
        }

        // POST: Crews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, SelectedBoatId")]CreateCrewViewModel crewVM)
        {
            if (ModelState.IsValid)
            {
                Crew crew = new Crew();

                crew.Name = crewVM.Name;
                crew.Captain = GetCurrentUser();
                crew.BoatId = crewVM.SelectedBoatId;
                crew.Id = Guid.NewGuid();

                crew.Crewmen = new List<CrewUser>()
                {
                    new CrewUser {Id = Guid.NewGuid(), User = GetCurrentUser(), CrewId = crew.Id }
                };

                _context.Add(crew);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(crewVM);
        }

        // GET: Crews/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crew = await _context.Crews.SingleOrDefaultAsync(m => m.Id == id);
            if (crew == null)
            {
                return NotFound();
            }
            return View(crew);
        }

        // POST: Crews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] Crew crew)
        {
            if (id != crew.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crew);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrewExists(crew.Id))
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
            return View(crew);
        }

        // GET: Crews/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crew = await _context.Crews
                .SingleOrDefaultAsync(m => m.Id == id);
            if (crew == null)
            {
                return NotFound();
            }

            return View(crew);
        }

        // POST: Crews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var crew = await _context.Crews.SingleOrDefaultAsync(m => m.Id == id);
            _context.Crews.Remove(crew);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrewExists(Guid id)
        {
            return _context.Crews.Any(e => e.Id == id);
        }

        #region Helpers

        public ApplicationUser GetCurrentUser()
        {
            return _context.Users.FirstOrDefault(u => u.UserName == HttpContext.User.Identity.Name);
        }

        #endregion

    }
}
