using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ROS.Web.Data;
using ROS.Web.Models;

namespace ROS.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/ApiRegatta")]
    public class ApiRegattaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApiRegattaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiRegatta
        [HttpGet]
        public IEnumerable<Regatta> GetRegattas()
        {
            return _context.Regattas;
        }

        // GET: api/ApiRegatta/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegatta([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var regatta = await _context.Regattas.SingleOrDefaultAsync(m => m.Id == id);

            if (regatta == null)
            {
                return NotFound();
            }

            return Ok(regatta);
        }
    }
}