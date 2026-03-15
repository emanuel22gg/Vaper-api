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
    public class DetalleComprasController : ControllerBase
    {
        private readonly VaperContext _context;

        public DetalleComprasController(VaperContext context)
        {
            _context = context;
        }

        // ===========================
        // ✅ DTO PARA OPTIMIZAR JSON
        // ===========================
        public class DetalleCompraDto
        {
            public int Id { get; set; }
            public int? CompraId { get; set; }
            public int? ProductoId { get; set; }
            public int? Cantidad { get; set; }
            public decimal? PrecioUnitario { get; set; }
            public decimal? Subtotal { get; set; }
        }

        // GET: api/DetalleCompras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleCompraDto>>> GetDetalleCompras()
        {
            var detalles = await _context.DetalleCompras.ToListAsync();
            return detalles.Select(d => new DetalleCompraDto
            {
                Id = d.Id,
                CompraId = d.CompraId,
                ProductoId = d.ProductoId,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                Subtotal = d.Subtotal
            }).ToList();
        }

        // GET: api/DetalleCompras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleCompraDto>> GetDetalleCompra(int id)
        {
            var d = await _context.DetalleCompras.FindAsync(id);

            if (d == null)
            {
                return NotFound();
            }

            return new DetalleCompraDto
            {
                Id = d.Id,
                CompraId = d.CompraId,
                ProductoId = d.ProductoId,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                Subtotal = d.Subtotal
            };
        }

        // PUT: api/DetalleCompras/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleCompra(int id, DetalleCompraDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var detalle = await _context.DetalleCompras.FindAsync(id);
            if (detalle == null)
            {
                return NotFound();
            }

            detalle.CompraId = dto.CompraId;
            detalle.ProductoId = dto.ProductoId;
            detalle.Cantidad = dto.Cantidad;
            detalle.PrecioUnitario = dto.PrecioUnitario;
            detalle.Subtotal = dto.Subtotal;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleCompraExists(id))
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

        // POST: api/DetalleCompras
        [HttpPost]
        public async Task<ActionResult<DetalleCompraDto>> PostDetalleCompra(DetalleCompraDto dto)
        {
            var detalle = new DetalleCompra
            {
                CompraId = dto.CompraId,
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario,
                Subtotal = dto.Subtotal
            };

            _context.DetalleCompras.Add(detalle);
            await _context.SaveChangesAsync();

            dto.Id = detalle.Id;

            return CreatedAtAction("GetDetalleCompra", new { id = detalle.Id }, dto);
        }

        // DELETE: api/DetalleCompras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleCompra(int id)
        {
            var detalleCompra = await _context.DetalleCompras.FindAsync(id);
            if (detalleCompra == null)
            {
                return NotFound();
            }

            _context.DetalleCompras.Remove(detalleCompra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleCompraExists(int id)
        {
            return _context.DetalleCompras.Any(e => e.Id == id);
        }
    }
}
