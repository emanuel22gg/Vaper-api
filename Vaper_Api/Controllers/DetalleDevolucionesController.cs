using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vaper_Api.Models;

namespace Vaper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleDevolucionesController : ControllerBase
    {
        private readonly VaperContext _context;

        public DetalleDevolucionesController(VaperContext context)
        {
            _context = context;
        }

        // GET: api/DetalleDevoluciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleDevolucione>>> GetDetalleDevoluciones()
        {
            return await _context.DetalleDevoluciones.ToListAsync();
        }

        // GET: api/DetalleDevoluciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleDevolucione>> GetDetalleDevolucione(int id)
        {
            var detalleDevolucione = await _context.DetalleDevoluciones.FindAsync(id);

            if (detalleDevolucione == null)
            {
                return NotFound();
            }

            return detalleDevolucione;
        }

        // PUT: api/DetalleDevoluciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleDevolucione(int id, DetalleDevolucione detalleDevolucione)
        {
            if (id != detalleDevolucione.Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleDevolucione).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleDevolucioneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DetalleDevoluciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleDevolucione>> PostDetalleDevolucione(DetalleDevolucione detalleDevolucione)
        {
            _context.DetalleDevoluciones.Add(detalleDevolucione);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalleDevolucione", new { id = detalleDevolucione.Id }, detalleDevolucione);
        }

        // DELETE: api/DetalleDevoluciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleDevolucione(int id)
        {
            var detalleDevolucione = await _context.DetalleDevoluciones.FindAsync(id);
            if (detalleDevolucione == null)
            {
                return NotFound();
            }

            _context.DetalleDevoluciones.Remove(detalleDevolucione);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleDevolucioneExists(int id)
        {
            return _context.DetalleDevoluciones.Any(e => e.Id == id);
        }
    }
}
