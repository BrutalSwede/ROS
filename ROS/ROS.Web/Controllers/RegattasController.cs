using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ROS.Web.Data;
using ROS.Web.Models;

namespace ROS.Web.Controllers
{
    public class RegattasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegattasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Regattas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Regattas.ToListAsync());
        }

        // GET: Regattas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regatta = await _context.Regattas
                .SingleOrDefaultAsync(m => m.Id == id);
            if (regatta == null)
            {
                return NotFound();
            }

            return View(regatta);
        }

        // GET: Regattas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Regattas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Address,Id")] Regatta regatta)
        {
            if (ModelState.IsValid)
            {
                regatta.Id = Guid.NewGuid();
                _context.Add(regatta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(regatta);
        }

        // GET: Regattas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regatta = await _context.Regattas.SingleOrDefaultAsync(m => m.Id == id);
            if (regatta == null)
            {
                return NotFound();
            }
            return View(regatta);
        }

        // POST: Regattas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Description,Address,Id")] Regatta regatta)
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
                .SingleOrDefaultAsync(m => m.Id == id);
            if (regatta == null)
            {
                return NotFound();
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
    }
}
