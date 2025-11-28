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
    public class DetalleCotizacionesController : ControllerBase
    {
        private readonly VaperContext _context;

        public DetalleCotizacionesController(VaperContext context)
        {
            _context = context;
        }

        // ===========================
        // ✅ DTO INTERNO
        // ===========================
        public class DetalleCotizacionDto
        {
            public int Id { get; set; }
            public int CotizacionId { get; set; }
            public int ProductoId { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public string? NombreProducto { get; set; }
        }

        // ===========================
        // ✅ GET: api/DetalleCotizaciones
        // ===========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleCotizacionDto>>> GetDetalleCotizaciones()
        {
            var detalles = await _context.DetalleCotizaciones
                .Include(d => d.Producto)
                .ToListAsync();

            return detalles.Select(d => new DetalleCotizacionDto
            {
                Id = d.Id,
                CotizacionId = d.CotizacionId,
                ProductoId = d.ProductoId,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                NombreProducto = d.Producto != null ? d.Producto.NombreProducto : null
            }).ToList();
        }

        // ===========================
        // ✅ GET: api/DetalleCotizaciones/5
        // ===========================
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleCotizacionDto>> GetDetalleCotizacion(int id)
        {
            var d = await _context.DetalleCotizaciones
                .Include(x => x.Producto)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (d == null)
                return NotFound();

            return new DetalleCotizacionDto
            {
                Id = d.Id,
                CotizacionId = d.CotizacionId,
                ProductoId = d.ProductoId,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                NombreProducto = d.Producto != null ? d.Producto.NombreProducto : null
            };
        }

        // ===========================
        // ✅ GET: api/DetalleCotizaciones/ByCotizacion/5
        // ===========================
        [HttpGet("ByCotizacion/{cotizacionId}")]
        public async Task<ActionResult<IEnumerable<DetalleCotizacionDto>>> GetDetallesByCotizacion(int cotizacionId)
        {
            var detalles = await _context.DetalleCotizaciones
                .Include(d => d.Producto)
                .Where(d => d.CotizacionId == cotizacionId)
                .ToListAsync();

            return detalles.Select(d => new DetalleCotizacionDto
            {
                Id = d.Id,
                CotizacionId = d.CotizacionId,
                ProductoId = d.ProductoId,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                NombreProducto = d.Producto != null ? d.Producto.NombreProducto : null
            }).ToList();
        }

        // ===========================
        // ✅ POST: api/DetalleCotizaciones
        // ===========================
        [HttpPost]
        public async Task<ActionResult<DetalleCotizacionDto>> PostDetalleCotizacion(DetalleCotizacionDto dto)
        {
            var detalle = new DetalleCotizacion
            {
                CotizacionId = dto.CotizacionId,
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario
            };

            _context.DetalleCotizaciones.Add(detalle);
            await _context.SaveChangesAsync();

            dto.Id = detalle.Id;

            return CreatedAtAction(nameof(GetDetalleCotizacion), new { id = detalle.Id }, dto);
        }

        // ===========================
        // ✅ PUT: api/DetalleCotizaciones/5
        // ===========================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleCotizacion(int id, DetalleCotizacionDto dto)
        {
            var detalle = await _context.DetalleCotizaciones.FindAsync(id);
            if (detalle == null)
                return NotFound();

            detalle.CotizacionId = dto.CotizacionId;
            detalle.ProductoId = dto.ProductoId;
            detalle.Cantidad = dto.Cantidad;
            detalle.PrecioUnitario = dto.PrecioUnitario;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ===========================
        // ✅ DELETE: api/DetalleCotizaciones/5
        // ===========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleCotizacion(int id)
        {
            var detalle = await _context.DetalleCotizaciones.FindAsync(id);
            if (detalle == null)
                return NotFound();

            _context.DetalleCotizaciones.Remove(detalle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}