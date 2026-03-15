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

        public class DetalleDevolucioneDto
        {
            public int Id { get; set; }
            public int? DevolucionId { get; set; }
            public int? DetalleVentaPedidoId { get; set; }
            public int? Cantidad { get; set; }
        }

        // GET: api/DetalleDevoluciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleDevolucioneDto>>> GetDetalleDevoluciones()
        {
            var detalles = await _context.DetalleDevoluciones.ToListAsync();
            return detalles.Select(d => new DetalleDevolucioneDto
            {
                Id = d.Id,
                DevolucionId = d.DevolucionId,
                DetalleVentaPedidoId = d.DetalleVentaPedidoId,
                Cantidad = d.Cantidad
            }).ToList();
        }

        // GET: api/DetalleDevoluciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleDevolucioneDto>> GetDetalleDevolucione(int id)
        {
            var d = await _context.DetalleDevoluciones.FindAsync(id);

            if (d == null)
            {
                return NotFound();
            }

            return new DetalleDevolucioneDto
            {
                Id = d.Id,
                DevolucionId = d.DevolucionId,
                DetalleVentaPedidoId = d.DetalleVentaPedidoId,
                Cantidad = d.Cantidad
            };
        }

        // PUT: api/DetalleDevoluciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleDevolucione(int id, DetalleDevolucioneDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var d = await _context.DetalleDevoluciones.FindAsync(id);
            if (d == null) return NotFound();

            d.DevolucionId = dto.DevolucionId;
            d.DetalleVentaPedidoId = dto.DetalleVentaPedidoId;
            d.Cantidad = dto.Cantidad;

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
        [HttpPost]
        public async Task<ActionResult<DetalleDevolucioneDto>> PostDetalleDevolucione(DetalleDevolucioneDto dto)
        {
            var detalleDevolucione = new DetalleDevolucione
            {
                DevolucionId = dto.DevolucionId,
                DetalleVentaPedidoId = dto.DetalleVentaPedidoId,
                Cantidad = dto.Cantidad
            };

            _context.DetalleDevoluciones.Add(detalleDevolucione);
            await _context.SaveChangesAsync();

            dto.Id = detalleDevolucione.Id;

            return CreatedAtAction("GetDetalleDevolucione", new { id = detalleDevolucione.Id }, dto);
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
