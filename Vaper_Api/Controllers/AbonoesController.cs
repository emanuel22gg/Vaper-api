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
    public class AbonoesController : ControllerBase
    {
        private readonly VaperContext _context;

        public AbonoesController(VaperContext context)
        {
            _context = context;
        }

        // =========================
        // ✅ DTO DENTRO DEL CONTROLADOR
        // =========================
        public class AbonoDto
        {
            public int Id { get; set; }
            public int VentaPedidoId { get; set; }
            public DateTime Fecha { get; set; }
            public decimal Monto { get; set; }
            public decimal SaldoRestante { get; set; }
            public string MetodoPago { get; set; }
            public bool Estado { get; set; }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AbonoDto>>> GetAbonos()
        {
            var abonos = await _context.Abonos.ToListAsync();
            return abonos.Select(a => new AbonoDto
            {
                Id = a.Id,
                VentaPedidoId = a.VentaPedidoId ?? 0,
                Fecha = a.Fecha ?? DateTime.MinValue,
                Monto = (decimal)(a.Monto ?? 0),
                SaldoRestante = (decimal)(a.SaldoRestante ?? 0),
                MetodoPago = a.MetodoPago,
                Estado = a.Estado ?? false
            }).ToList();
        }

        // =========================
        // GET: api/Abonoes/pedido/5
        // =========================
        [HttpGet("pedido/{pedidoId}")]
        public async Task<ActionResult<IEnumerable<AbonoDto>>> GetAbonosByPedido(int pedidoId)
        {
            var abonos = await _context.Abonos
                .Where(a => a.VentaPedidoId == pedidoId)
                .ToListAsync();

            return abonos.Select(a => new AbonoDto
            {
                Id = a.Id,
                VentaPedidoId = a.VentaPedidoId ?? 0,
                Fecha = a.Fecha ?? DateTime.MinValue,
                Monto = (decimal)(a.Monto ?? 0),
                SaldoRestante = (decimal)(a.SaldoRestante ?? 0),
                MetodoPago = a.MetodoPago,
                Estado = a.Estado ?? false
            }).ToList();
        }

        // =========================
        // GET: api/Abonoes/5
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<AbonoDto>> GetAbono(int id)
        {
            var a = await _context.Abonos.FindAsync(id);

            if (a == null)
            {
                return NotFound();
            }

            return new AbonoDto
            {
                Id = a.Id,
                VentaPedidoId = a.VentaPedidoId ?? 0,
                Fecha = a.Fecha ?? DateTime.MinValue,
                Monto = (decimal)(a.Monto ?? 0),
                SaldoRestante = (decimal)(a.SaldoRestante ?? 0),
                MetodoPago = a.MetodoPago,
                Estado = a.Estado ?? false
            };
        }

        // =========================
        // ✅ POST: SOLO RECIBE Y DEVUELVE DTO (Evita ciclos en Swagger)
        // =========================
        [HttpPost]
        public async Task<ActionResult<AbonoDto>> PostAbono(AbonoDto dto)
        {
            var abono = new Abono
            {
                VentaPedidoId = dto.VentaPedidoId,
                Fecha = DateTime.Now,
                Monto = dto.Monto,
                SaldoRestante = dto.SaldoRestante,
                MetodoPago = dto.MetodoPago,
                Estado = dto.Estado
            };

            _context.Abonos.Add(abono);
            await _context.SaveChangesAsync();

            // Retornar el DTO con el ID generado
            dto.Id = abono.Id;
            dto.Fecha = abono.Fecha ?? DateTime.Now;

            return CreatedAtAction(nameof(GetAbono), new { id = abono.Id }, dto);
        }

        // =========================
        // ✅ PUT: SOLO ACTUALIZA ESOS CAMPOS
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAbono(int id, AbonoDto dto)
        {
            var abono = await _context.Abonos.FindAsync(id);

            if (abono == null)
                return NotFound();

            abono.VentaPedidoId = dto.VentaPedidoId;
            abono.Fecha = dto.Fecha;
            abono.Monto = dto.Monto;
            abono.SaldoRestante = dto.SaldoRestante;
            abono.MetodoPago = dto.MetodoPago;
            abono.Estado = dto.Estado;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // =========================
        // DELETE: api/Abonoes/5
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbono(int id)
        {
            var abono = await _context.Abonos.FindAsync(id);
            if (abono == null)
            {
                return NotFound();
            }

            _context.Abonos.Remove(abono);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AbonoExists(int id)
        {
            return _context.Abonos.Any(e => e.Id == id);
        }
    }
}

