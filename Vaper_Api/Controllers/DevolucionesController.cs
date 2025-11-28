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
            public DateTime? Fecha { get; set; }
            public string? Descripcion { get; set; }
            public bool? Estado { get; set; }
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
                Fecha = d.Fecha,
                Descripcion = d.Descripcion,
                Estado = d.Estado
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
                Fecha = d.Fecha,
                Descripcion = d.Descripcion,
                Estado = d.Estado
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
                Fecha = dto.Fecha,
                Descripcion = dto.Descripcion,
                Estado = dto.Estado
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

            devolucion.Fecha = dto.Fecha;
            devolucion.Descripcion = dto.Descripcion;
            devolucion.Estado = dto.Estado;

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
