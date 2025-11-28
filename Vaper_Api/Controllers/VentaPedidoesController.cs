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
    public class VentaPedidosController : ControllerBase
    {
        private readonly VaperContext _context;

        public VentaPedidosController(VaperContext context)
        {
            _context = context;
        }

        // ===========================
        // ✅ DTO SOLO CON LOS CAMPOS QUE NECESITAS
        // ===========================
        public class VentaPedidoDto
        {
            public int Id { get; set; }
            public int? UsuarioId { get; set; }
            public int? EstadoId { get; set; }
            public DateTime? FechaCreacion { get; set; }
            public DateTime? FechaEntrega { get; set; }
            public string? MetodoPago { get; set; }
            public string? DireccionEntrega { get; set; }
            public string? CiudadEntrega { get; set; }
            public string? DepartamentoEntrega { get; set; }
            public decimal? Subtotal { get; set; }
            public decimal? Envio { get; set; }
            public decimal? Total { get; set; }
        }

        // ===========================
        // ✅ GET: api/VentaPedidos
        // ===========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentaPedidoDto>>> GetVentaPedidos()
        {
            var ventas = await _context.VentaPedidos.ToListAsync();

            return ventas.Select(v => new VentaPedidoDto
            {
                Id = v.Id,
                UsuarioId = v.UsuarioId,
                EstadoId = v.EstadoId,
                FechaCreacion = v.FechaCreacion,
                FechaEntrega = v.FechaEntrega,
                MetodoPago = v.MetodoPago,
                DireccionEntrega = v.DireccionEntrega,
                CiudadEntrega = v.CiudadEntrega,
                DepartamentoEntrega = v.DepartamentoEntrega,
                Subtotal = v.Subtotal,
                Envio = v.Envio,
                Total = v.Total
            }).ToList();
        }

        // ===========================
        // ✅ GET: api/VentaPedidos/5
        // ===========================
        [HttpGet("{id}")]
        public async Task<ActionResult<VentaPedidoDto>> GetVentaPedido(int id)
        {
            var v = await _context.VentaPedidos.FindAsync(id);
            if (v == null)
                return NotFound();

            return new VentaPedidoDto
            {
                Id = v.Id,
                UsuarioId = v.UsuarioId,
                EstadoId = v.EstadoId,
                FechaCreacion = v.FechaCreacion,
                FechaEntrega = v.FechaEntrega,
                MetodoPago = v.MetodoPago,
                DireccionEntrega = v.DireccionEntrega,
                CiudadEntrega = v.CiudadEntrega,
                DepartamentoEntrega = v.DepartamentoEntrega,
                Subtotal = v.Subtotal,
                Envio = v.Envio,
                Total = v.Total
            };
        }

        // ===========================
        // ✅ POST: api/VentaPedidos
        // ===========================
        [HttpPost]
        public async Task<ActionResult<VentaPedidoDto>> PostVentaPedido(VentaPedidoDto dto)
        {
            var venta = new VentaPedido
            {
                UsuarioId = dto.UsuarioId,
                EstadoId = dto.EstadoId,
                FechaCreacion = dto.FechaCreacion,
                FechaEntrega = dto.FechaEntrega,
                MetodoPago = dto.MetodoPago,
                DireccionEntrega = dto.DireccionEntrega,
                CiudadEntrega = dto.CiudadEntrega,
                DepartamentoEntrega = dto.DepartamentoEntrega,
                Subtotal = dto.Subtotal,
                Envio = dto.Envio,
                Total = dto.Total
            };

            _context.VentaPedidos.Add(venta);
            await _context.SaveChangesAsync();

            dto.Id = venta.Id;

            return CreatedAtAction(nameof(GetVentaPedido), new { id = venta.Id }, dto);
        }

        // ===========================
        // ✅ PUT: api/VentaPedidos/5
        // ===========================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVentaPedido(int id, VentaPedidoDto dto)
        {
            var venta = await _context.VentaPedidos.FindAsync(id);
            if (venta == null)
                return NotFound();

            venta.UsuarioId = dto.UsuarioId;
            venta.EstadoId = dto.EstadoId;
            venta.FechaCreacion = dto.FechaCreacion;
            venta.FechaEntrega = dto.FechaEntrega;
            venta.MetodoPago = dto.MetodoPago;
            venta.DireccionEntrega = dto.DireccionEntrega;
            venta.CiudadEntrega = dto.CiudadEntrega;
            venta.DepartamentoEntrega = dto.DepartamentoEntrega;
            venta.Subtotal = dto.Subtotal;
            venta.Envio = dto.Envio;
            venta.Total = dto.Total;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ===========================
        // ✅ DELETE: api/VentaPedidos/5
        // ===========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVentaPedido(int id)
        {
            var venta = await _context.VentaPedidos.FindAsync(id);
            if (venta == null)
                return NotFound();

            _context.VentaPedidos.Remove(venta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
