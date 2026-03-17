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
    public class ComprasController : ControllerBase
    {
        private readonly VaperContext _context;

        public ComprasController(VaperContext context)
        {
            _context = context;
        }

        // =========================
        // ✅ DTO DENTRO DEL CONTROLADOR
        // =========================
        public class CompraDto
        {
            public int Id { get; set; }
            public string NumeroCompra { get; set; } = null!;
            public string? NumeroFactura { get; set; }
            public DateTime? FechaCompra { get; set; }
            public int? ProveedorId { get; set; }
            public decimal? Subtotal { get; set; }
            public decimal? Total { get; set; }
            public int? Estado { get; set; }
            public string? Observaciones { get; set; }
            public DateTime? FechaRegistro { get; set; }
            public List<DetalleCompraItemDto> DetalleCompras { get; set; } = new();
        }

        public class DetalleCompraItemDto
        {
            public int ProductoId { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal Subtotal { get; set; }
        }

        // =========================
        // GET: api/Compras
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompraDto>>> GetCompras()
        {
            var compras = await _context.Compras
                .Include(c => c.DetalleCompras)
                .ToListAsync();

            return compras.Select(c => new CompraDto
            {
                Id = c.Id,
                NumeroCompra = c.NumeroCompra,
                NumeroFactura = c.NumeroFactura,
                FechaCompra = c.FechaCompra,
                ProveedorId = c.ProveedorId,
                Subtotal = c.Subtotal,
                Total = c.Total,
                Estado = c.Estado,
                Observaciones = c.Observaciones,
                FechaRegistro = c.FechaRegistro,
                DetalleCompras = c.DetalleCompras.Select(d => new DetalleCompraItemDto
                {
                    ProductoId = d.ProductoId ?? 0,
                    Cantidad = d.Cantidad ?? 0,
                    PrecioUnitario = d.PrecioUnitario ?? 0,
                    Subtotal = d.Subtotal ?? 0
                }).ToList()
            }).ToList();
        }

        // =========================
        // GET: api/Compras/5
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<CompraDto>> GetCompra(int id)
        {
            var c = await _context.Compras
                .Include(c => c.DetalleCompras)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (c == null) return NotFound();

            return new CompraDto
            {
                Id = c.Id,
                NumeroCompra = c.NumeroCompra,
                NumeroFactura = c.NumeroFactura,
                FechaCompra = c.FechaCompra,
                ProveedorId = c.ProveedorId,
                Subtotal = c.Subtotal,
                Total = c.Total,
                Estado = c.Estado,
                Observaciones = c.Observaciones,
                FechaRegistro = c.FechaRegistro,
                DetalleCompras = c.DetalleCompras.Select(d => new DetalleCompraItemDto
                {
                    ProductoId = d.ProductoId ?? 0,
                    Cantidad = d.Cantidad ?? 0,
                    PrecioUnitario = d.PrecioUnitario ?? 0,
                    Subtotal = d.Subtotal ?? 0
                }).ToList()
            };
        }

        // post
        [HttpPost]
        public async Task<ActionResult<CompraDto>> PostCompra(CompraDto dto)
        {
            var compra = new Compra
            {
                NumeroCompra = dto.NumeroCompra,
                NumeroFactura = dto.NumeroFactura,
                FechaCompra = dto.FechaCompra,
                ProveedorId = dto.ProveedorId,
                Subtotal = dto.Subtotal,
                Total = dto.Total,
                Estado = dto.Estado,
                Observaciones = dto.Observaciones,
                FechaRegistro = dto.FechaRegistro ?? DateTime.Now
            };

            // Mapeo de detalles - Aseguramos que la lista existe
            if (dto.DetalleCompras != null)
            {
                foreach (var detalleDto in dto.DetalleCompras)
                {
                    compra.DetalleCompras.Add(new DetalleCompra
                    {
                        ProductoId = detalleDto.ProductoId,
                        Cantidad = detalleDto.Cantidad,
                        PrecioUnitario = detalleDto.PrecioUnitario,
                        Subtotal = detalleDto.Subtotal
                    });
                }
            }
            
            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();

            // Mapeamos de vuelta al DTO para incluir lo que realmente se guardó
            var responseDto = new CompraDto
            {
                Id = compra.Id,
                NumeroCompra = compra.NumeroCompra,
                NumeroFactura = compra.NumeroFactura,
                FechaCompra = compra.FechaCompra,
                ProveedorId = compra.ProveedorId,
                Subtotal = compra.Subtotal,
                Total = compra.Total,
                Estado = compra.Estado,
                Observaciones = compra.Observaciones,
                FechaRegistro = compra.FechaRegistro,
                DetalleCompras = compra.DetalleCompras.Select(d => new DetalleCompraItemDto
                {
                    ProductoId = d.ProductoId ?? 0,
                    Cantidad = d.Cantidad ?? 0,
                    PrecioUnitario = d.PrecioUnitario ?? 0,
                    Subtotal = d.Subtotal ?? 0
                }).ToList()
            };

            return CreatedAtAction("GetCompra", new { id = compra.Id }, responseDto);
        }

        // =========================
        // PUT: api/Compras/5
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompra(int id, CompraDto dto)
        {
            var compra = await _context.Compras.FindAsync(id);
            if (compra == null) return NotFound();

            compra.NumeroCompra = dto.NumeroCompra;
            compra.NumeroFactura = dto.NumeroFactura;
            compra.FechaCompra = dto.FechaCompra;
            compra.ProveedorId = dto.ProveedorId;
            compra.Subtotal = dto.Subtotal;
            compra.Total = dto.Total;
            compra.Estado = dto.Estado;
            compra.Observaciones = dto.Observaciones;
            compra.FechaRegistro = dto.FechaRegistro;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // =========================
        // DELETE: api/Compras/5
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra(int id)
        {
            var compra = await _context.Compras.FindAsync(id);
            if (compra == null) return NotFound();

            _context.Compras.Remove(compra);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
