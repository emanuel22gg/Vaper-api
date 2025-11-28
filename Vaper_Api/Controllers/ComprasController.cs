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
        // ✅ DTO DENTRO DEL CONTROLADOR (IGUAL A TU MODELO)
        // =========================
        public class CompraDto
        {
            public int Id { get; set; }
            public string NumeroCompra { get; set; } = null!;
            public DateTime? FechaCompra { get; set; }
            public int? ProveedorId { get; set; }
            public decimal? Subtotal { get; set; }
            public decimal? Total { get; set; }
            public int? Estado { get; set; }
            public string? Observaciones { get; set; }
            public DateTime? FechaCreacion { get; set; }
        }

        // =========================
        // GET: api/Compras
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompraDto>>> GetCompras()
        {
            var compras = await _context.Compras.ToListAsync();

            return compras.Select(c => new CompraDto
            {
                Id = c.Id,
                NumeroCompra = c.NumeroCompra,
                FechaCompra = c.FechaCompra,
                ProveedorId = c.ProveedorId,
                Subtotal = c.Subtotal,
                Total = c.Total,
                Estado = c.Estado,
                Observaciones = c.Observaciones,
                FechaCreacion = c.FechaCreacion
            }).ToList();
        }

        // =========================
        // GET: api/Compras/5
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<CompraDto>> GetCompra(int id)
        {
            var c = await _context.Compras.FindAsync(id);
            if (c == null) return NotFound();

            return new CompraDto
            {
                Id = c.Id,
                NumeroCompra = c.NumeroCompra,
                FechaCompra = c.FechaCompra,
                ProveedorId = c.ProveedorId,
                Subtotal = c.Subtotal,
                Total = c.Total,
                Estado = c.Estado,
                Observaciones = c.Observaciones,
                FechaCreacion = c.FechaCreacion
            };
        }

        // =========================
        // POST: api/Compras
        // =========================
        [HttpPost]
        public async Task<ActionResult<CompraDto>> PostCompra(CompraDto dto)
        {
            var compra = new Compra
            {
                NumeroCompra = dto.NumeroCompra,
                FechaCompra = dto.FechaCompra,
                ProveedorId = dto.ProveedorId,
                Subtotal = dto.Subtotal,
                Total = dto.Total,
                Estado = dto.Estado,
                Observaciones = dto.Observaciones,
                FechaCreacion = dto.FechaCreacion
            };

            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();

            dto.Id = compra.Id; // ✅ Devuelve el ID generado

            return CreatedAtAction("GetCompra", new { id = compra.Id }, dto);
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
            compra.FechaCompra = dto.FechaCompra;
            compra.ProveedorId = dto.ProveedorId;
            compra.Subtotal = dto.Subtotal;
            compra.Total = dto.Total;
            compra.Estado = dto.Estado;
            compra.Observaciones = dto.Observaciones;
            compra.FechaCreacion = dto.FechaCreacion;

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
