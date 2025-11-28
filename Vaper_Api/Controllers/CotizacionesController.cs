using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vaper_Api.Models;

namespace Vaper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionesController : ControllerBase
    {
        private readonly VaperContext _context;

        public CotizacionesController(VaperContext context)
        {
            _context = context;
        }

        // ===========================
        // ✅ DTO INTERNO
        // ===========================
        public class CotizacionDto
        {
            public int Id { get; set; }
            public string? NombreUsuario { get; set; }
            public DateTime Fecha { get; set; }
            public decimal Total { get; set; }
        }

        // ===========================
        // ✅ GET: api/Cotizaciones
        // ===========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CotizacionDto>>> GetCotizaciones()
        {
            var cotizaciones = await _context.Cotizaciones.ToListAsync();

            return cotizaciones.Select(c => new CotizacionDto
            {
                Id = c.Id,
                NombreUsuario = c.NombreUsuario,
                Fecha = c.Fecha,
                Total = c.Total
            }).ToList();
        }

        // ===========================
        // ✅ GET: api/Cotizaciones/5
        // ===========================
        [HttpGet("{id}")]
        public async Task<ActionResult<CotizacionDto>> GetCotizacion(int id)
        {
            var c = await _context.Cotizaciones.FirstOrDefaultAsync(x => x.Id == id);

            if (c == null)
                return NotFound();

            return new CotizacionDto
            {
                Id = c.Id,
                NombreUsuario = c.NombreUsuario,
                Fecha = c.Fecha,
                Total = c.Total
            };
        }

        // ===========================
        // ✅ POST: api/Cotizaciones
        // ===========================
        [HttpPost]
        public async Task<ActionResult<CotizacionDto>> PostCotizacion(CotizacionDto dto)
        {
            var cotizacion = new Cotizacion
            {
                NombreUsuario = dto.NombreUsuario,
                Fecha = DateTime.Now,
                Total = dto.Total
            };

            _context.Cotizaciones.Add(cotizacion);
            await _context.SaveChangesAsync();

            dto.Id = cotizacion.Id;
            dto.Fecha = cotizacion.Fecha;

            return CreatedAtAction(nameof(GetCotizacion), new { id = cotizacion.Id }, dto);
        }

        // ===========================
        // ✅ PUT: api/Cotizaciones/5
        // ===========================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCotizacion(int id, CotizacionDto dto)
        {
            var cotizacion = await _context.Cotizaciones.FindAsync(id);
            if (cotizacion == null)
                return NotFound();

            cotizacion.NombreUsuario = dto.NombreUsuario;
            cotizacion.Total = dto.Total;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ===========================
        // ✅ DELETE: api/Cotizaciones/5
        // ===========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCotizacion(int id)
        {
            var cotizacion = await _context.Cotizaciones.FindAsync(id);
            if (cotizacion == null)
                return NotFound();

            _context.Cotizaciones.Remove(cotizacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}