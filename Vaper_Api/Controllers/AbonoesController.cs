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
            public int VentaPedidoId { get; set; }
            public DateTime Fecha { get; set; }
            public decimal Monto { get; set; }
            public decimal SaldoRestante { get; set; }
            public string MetodoPago { get; set; }
            public bool Estado { get; set; }
        }

        // =========================
        // GET: api/Abonoes
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Abono>>> GetAbonos()
        {
            return await _context.Abonos.ToListAsync();
        }

        // =========================
        // GET: api/Abonoes/5
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<Abono>> GetAbono(int id)
        {
            var abono = await _context.Abonos.FindAsync(id);

            if (abono == null)
            {
                return NotFound();
            }

            return abono;
        }

        // =========================
        // ✅ POST: SOLO RECIBE LOS CAMPOS QUE PEDISTE
        // =========================
        [HttpPost]
        public async Task<ActionResult<Abono>> PostAbono(AbonoDto dto)
        {
            var abono = new Abono
            {
                VentaPedidoId = dto.VentaPedidoId,
                Fecha = dto.Fecha,
                Monto = dto.Monto,
                SaldoRestante = dto.SaldoRestante,
                MetodoPago = dto.MetodoPago,
                Estado = dto.Estado
            };

            _context.Abonos.Add(abono);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAbono", new { id = abono.Id }, abono);
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

