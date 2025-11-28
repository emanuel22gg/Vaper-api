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
    public class PermisoesController : ControllerBase
    {
        private readonly VaperContext _context;

        public PermisoesController(VaperContext context)
        {
            _context = context;
        }

        // ===========================
        // ✅ DTO INTERNO
        // ===========================
        public class PermisoDto
        {
            public int Id { get; set; }
            public string? NombrePermiso { get; set; }
        }

        // ===========================
        // ✅ GET: api/Permisoes
        // ===========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermisoDto>>> GetPermisos()
        {
            var permisos = await _context.Permisos.ToListAsync();

            return permisos.Select(p => new PermisoDto
            {
                Id = p.Id,
                NombrePermiso = p.NombrePermiso
            }).ToList();
        }

        // ===========================
        // ✅ GET: api/Permisoes/5
        // ===========================
        [HttpGet("{id}")]
        public async Task<ActionResult<PermisoDto>> GetPermiso(int id)
        {
            var permiso = await _context.Permisos.FindAsync(id);
            if (permiso == null)
                return NotFound();

            return new PermisoDto
            {
                Id = permiso.Id,
                NombrePermiso = permiso.NombrePermiso
            };
        }

        // ===========================
        // ✅ POST: api/Permisoes
        // ===========================
        [HttpPost]
        public async Task<ActionResult<PermisoDto>> PostPermiso(PermisoDto dto)
        {
            var permiso = new Permiso
            {
                NombrePermiso = dto.NombrePermiso
            };

            _context.Permisos.Add(permiso);
            await _context.SaveChangesAsync();

            dto.Id = permiso.Id;

            return CreatedAtAction(nameof(GetPermiso), new { id = permiso.Id }, dto);
        }

        // ===========================
        // ✅ PUT: api/Permisoes/5
        // ===========================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermiso(int id, PermisoDto dto)
        {
            var permiso = await _context.Permisos.FindAsync(id);
            if (permiso == null)
                return NotFound();

            permiso.NombrePermiso = dto.NombrePermiso;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ===========================
        // ✅ DELETE: api/Permisoes/5
        // ===========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermiso(int id)
        {
            var permiso = await _context.Permisos.FindAsync(id);
            if (permiso == null)
                return NotFound();

            _context.Permisos.Remove(permiso);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
