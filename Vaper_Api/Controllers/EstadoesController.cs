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
    public class EstadoesController : ControllerBase
    {
        private readonly VaperContext _context;

        public EstadoesController(VaperContext context)
        {
            _context = context;
        }

        // =========================
        // ✅ DTO DENTRO DEL CONTROLADOR (CON ID)
        // =========================
        public class EstadoDto
        {
            public int Id { get; set; }
            public string NombreEstado { get; set; } = null!;
        }

        // =========================
        // GET: api/Estadoes
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoDto>>> GetEstados()
        {
            var estados = await _context.Estados.ToListAsync();

            return estados.Select(e => new EstadoDto
            {
                Id = e.Id,
                NombreEstado = e.NombreEstado
            }).ToList();
        }

        // =========================
        // GET: api/Estadoes/5
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoDto>> GetEstado(int id)
        {
            var e = await _context.Estados.FindAsync(id);
            if (e == null) return NotFound();

            return new EstadoDto
            {
                Id = e.Id,
                NombreEstado = e.NombreEstado
            };
        }

        // =========================
        // POST: api/Estadoes
        // =========================
        [HttpPost]
        public async Task<ActionResult<EstadoDto>> PostEstado(EstadoDto dto)
        {
            var estado = new Estado
            {
                NombreEstado = dto.NombreEstado
            };

            _context.Estados.Add(estado);
            await _context.SaveChangesAsync();

            dto.Id = estado.Id; // ✅ devuelve el ID generado

            return CreatedAtAction("GetEstado", new { id = estado.Id }, dto);
        }

        // =========================
        // PUT: api/Estadoes/5
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, EstadoDto dto)
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null) return NotFound();

            estado.NombreEstado = dto.NombreEstado;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // =========================
        // DELETE: api/Estadoes/5
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null) return NotFound();

            _context.Estados.Remove(estado);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
