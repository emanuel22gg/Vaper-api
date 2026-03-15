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
    public class DetalleVentaPedidoesController : ControllerBase
    {
        private readonly VaperContext _context;

        public DetalleVentaPedidoesController(VaperContext context)
        {
            _context = context;
        }

        // ===========================
        // ✅ DTO PARA OPTIMIZAR JSON
        // ===========================
        public class DetalleVentaPedidoDto
        {
            public int Id { get; set; }
            public int? VentaPedidoId { get; set; }
            public int? ProductoId { get; set; }
            public int? Cantidad { get; set; }
            public decimal? PrecioUnitario { get; set; }
            public decimal? Subtotal { get; set; }
        }

        // GET: api/DetalleVentaPedidoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleVentaPedidoDto>>> GetDetalleVentaPedidos()
        {
            var detalles = await _context.DetalleVentaPedidos.ToListAsync();
            return detalles.Select(d => new DetalleVentaPedidoDto
            {
                Id = d.Id,
                VentaPedidoId = d.VentaPedidoId,
                ProductoId = d.ProductoId,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                Subtotal = d.Subtotal
            }).ToList();
        }

        // GET: api/DetalleVentaPedidoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleVentaPedidoDto>> GetDetalleVentaPedido(int id)
        {
            var d = await _context.DetalleVentaPedidos.FindAsync(id);

            if (d == null)
            {
                return NotFound();
            }

            return new DetalleVentaPedidoDto
            {
                Id = d.Id,
                VentaPedidoId = d.VentaPedidoId,
                ProductoId = d.ProductoId,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                Subtotal = d.Subtotal
            };
        }

        // PUT: api/DetalleVentaPedidoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleVentaPedido(int id, DetalleVentaPedidoDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var detalle = await _context.DetalleVentaPedidos.FindAsync(id);
            if (detalle == null)
            {
                return NotFound();
            }

            detalle.VentaPedidoId = dto.VentaPedidoId;
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
                if (!DetalleVentaPedidoExists(id))
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

        // POST: api/DetalleVentaPedidoes
        [HttpPost]
        public async Task<ActionResult<DetalleVentaPedidoDto>> PostDetalleVentaPedido(DetalleVentaPedidoDto dto)
        {
            var detalle = new DetalleVentaPedido
            {
                VentaPedidoId = dto.VentaPedidoId,
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario,
                Subtotal = dto.Subtotal
            };

            _context.DetalleVentaPedidos.Add(detalle);
            await _context.SaveChangesAsync();

            dto.Id = detalle.Id;

            return CreatedAtAction("GetDetalleVentaPedido", new { id = detalle.Id }, dto);
        }

        // DELETE: api/DetalleVentaPedidoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleVentaPedido(int id)
        {
            var detalleVentaPedido = await _context.DetalleVentaPedidos.FindAsync(id);
            if (detalleVentaPedido == null)
            {
                return NotFound();
            }

            _context.DetalleVentaPedidos.Remove(detalleVentaPedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleVentaPedidoExists(int id)
        {
            return _context.DetalleVentaPedidos.Any(e => e.Id == id);
        }
    }
}
