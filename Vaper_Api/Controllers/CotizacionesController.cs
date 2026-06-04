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
            public int? Vigencia { get; set; }
            public int? EstadoId { get; set; }
            public string? NombreEstado { get; set; }
            public decimal? Subtotal { get; set; }
            public decimal? Descuento { get; set; }
        }

        // ===========================
        // ✅ GET: api/Cotizaciones
        // ===========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CotizacionDto>>> GetCotizaciones()
        {
            var cotizaciones = await _context.Cotizaciones.Include(c => c.Estado).ToListAsync();
            bool guardadoNecesario = false;

            foreach (var c in cotizaciones)
            {
                if (c.EstadoId == 9) // 9 = Vigente
                {
                    var vigenciaDias = c.Vigencia ?? 0;
                    if (c.Fecha.AddDays(vigenciaDias) < DateTime.Now)
                    {
                        c.EstadoId = 10; // 10 = Expirada
                        guardadoNecesario = true;
                    }
                }
            }

            if (guardadoNecesario)
            {
                await _context.SaveChangesAsync();
                // Recargar para obtener el NombreEstado actualizado
                cotizaciones = await _context.Cotizaciones.Include(c => c.Estado).ToListAsync();
            }

            return cotizaciones.Select(c => new CotizacionDto
            {
                Id = c.Id,
                NombreUsuario = c.NombreUsuario,
                Fecha = c.Fecha,
                Total = c.Total,
                Vigencia = c.Vigencia,
                EstadoId = c.EstadoId,
                NombreEstado = c.Estado?.NombreEstado,
                Subtotal = c.Subtotal,
                Descuento = c.Descuento
            }).ToList();
        }

        // ===========================
        // ✅ GET: api/Cotizaciones/5
        // ===========================
        [HttpGet("{id}")]
        public async Task<ActionResult<CotizacionDto>> GetCotizacion(int id)
        {
            var c = await _context.Cotizaciones.Include(x => x.Estado).FirstOrDefaultAsync(x => x.Id == id);

            if (c == null)
                return NotFound();

            // Validación de expiración individual
            if (c.EstadoId == 9) // Vigente
            {
                var vigenciaDias = c.Vigencia ?? 0;
                if (c.Fecha.AddDays(vigenciaDias) < DateTime.Now)
                {
                    c.EstadoId = 10; // Expirada
                    await _context.SaveChangesAsync();
                    // Refrescar entidad para el NombreEstado si es necesario (Entity Framework ya debería trackearlo)
                    c = await _context.Cotizaciones.Include(x => x.Estado).FirstOrDefaultAsync(x => x.Id == id);
                }
            }

            return new CotizacionDto
            {
                Id = c!.Id,
                NombreUsuario = c.NombreUsuario,
                Fecha = c.Fecha,
                Total = c.Total,
                Vigencia = c.Vigencia,
                EstadoId = c.EstadoId,
                NombreEstado = c.Estado?.NombreEstado,
                Subtotal = c.Subtotal,
                Descuento = c.Descuento
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
                Total = dto.Total,
                Vigencia = dto.Vigencia,
                EstadoId = dto.EstadoId,
                Subtotal = dto.Subtotal,
                Descuento = dto.Descuento
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
            cotizacion.Vigencia = dto.Vigencia;
            cotizacion.EstadoId = dto.EstadoId;
            cotizacion.Subtotal = dto.Subtotal;
            cotizacion.Descuento = dto.Descuento;

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