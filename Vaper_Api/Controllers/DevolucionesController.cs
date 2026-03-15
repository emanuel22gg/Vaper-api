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
    public class DevolucionesController : ControllerBase
    {
        private readonly VaperContext _context;

        public DevolucionesController(VaperContext context)
        {
            _context = context;
        }

        // =========================
        // ✅ DTO DENTRO DEL CONTROLADOR (CON ID)
        // =========================
        public class DevolucioneDto
        {
            public int Id { get; set; }
            public DateTime? FechaDevolucion { get; set; }
            public string? Descripcion { get; set; }
            public int? VentaPedidoId { get; set; }
            public decimal? MontoTotal { get; set; }
            public int? EstadoId { get; set; }
        }

        // =========================
        // GET: api/Devoluciones
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DevolucioneDto>>> GetDevoluciones()
        {
            var devoluciones = await _context.Devoluciones.ToListAsync();

            return devoluciones.Select(d => new DevolucioneDto
            {
                Id = d.Id,
                FechaDevolucion = d.FechaDevolucion,
                Descripcion = d.Descripcion,
                VentaPedidoId = d.VentaPedidoId,
                MontoTotal = d.MontoTotal,
                EstadoId = d.EstadoId
            }).ToList();
        }

        // =========================
        // GET: api/Devoluciones/5
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<DevolucioneDto>> GetDevolucione(int id)
        {
            var d = await _context.Devoluciones.FindAsync(id);
            if (d == null) return NotFound();

            return new DevolucioneDto
            {
                Id = d.Id,
                FechaDevolucion = d.FechaDevolucion,
                Descripcion = d.Descripcion,
                VentaPedidoId = d.VentaPedidoId,
                MontoTotal = d.MontoTotal,
                EstadoId = d.EstadoId
            };
        }

        // =========================
        // POST: api/Devoluciones
        // =========================
        [HttpPost]
        public async Task<ActionResult<DevolucioneDto>> PostDevolucione(DevolucioneDto dto)
        {
            var devolucion = new Devolucione
            {
                FechaDevolucion = dto.FechaDevolucion,
                Descripcion = dto.Descripcion,
                VentaPedidoId = dto.VentaPedidoId,
                MontoTotal = dto.MontoTotal,
                EstadoId = dto.EstadoId
            };

            _context.Devoluciones.Add(devolucion);
            await _context.SaveChangesAsync();

            dto.Id = devolucion.Id; // ✅ devuelve el ID generado

            return CreatedAtAction("GetDevolucione", new { id = devolucion.Id }, dto);
        }

        // =========================
        // PUT: api/Devoluciones/5
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevolucione(int id, DevolucioneDto dto)
        {
            var devolucion = await _context.Devoluciones.FindAsync(id);
            if (devolucion == null) return NotFound();

            devolucion.FechaDevolucion = dto.FechaDevolucion;
            devolucion.Descripcion = dto.Descripcion;
            devolucion.VentaPedidoId = dto.VentaPedidoId;
            devolucion.MontoTotal = dto.MontoTotal;
            devolucion.EstadoId = dto.EstadoId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // =========================
        // DELETE: api/Devoluciones/5
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevolucione(int id)
        {
            var devolucion = await _context.Devoluciones.FindAsync(id);
            if (devolucion == null) return NotFound();

            _context.Devoluciones.Remove(devolucion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
