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
    public class RolesPermisoesController : ControllerBase
    {
        private readonly VaperContext _context;

        public RolesPermisoesController(VaperContext context)
        {
            _context = context;
        }

        // ===========================
        // ✅ DTO INTERNO
        // ===========================
        public class RolPermisoDto
        {
            public int Id { get; set; }
            public int? RolId { get; set; }
            public int? PermisoId { get; set; }
        }

        // ===========================
        // ✅ GET: api/RolesPermisoes
        // ===========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolPermisoDto>>> GetRolesPermisos()
        {
            var lista = await _context.RolesPermisos.ToListAsync();

            return lista.Select(rp => new RolPermisoDto
            {
                Id = rp.Id,
                RolId = rp.RolId,
                PermisoId = rp.PermisoId
            }).ToList();
        }

        // ===========================
        // ✅ GET: api/RolesPermisoes/5
        // ===========================
        [HttpGet("{id}")]
        public async Task<ActionResult<RolPermisoDto>> GetRolesPermiso(int id)
        {
            var rp = await _context.RolesPermisos.FindAsync(id);
            if (rp == null)
                return NotFound();

            return new RolPermisoDto
            {
                Id = rp.Id,
                RolId = rp.RolId,
                PermisoId = rp.PermisoId
            };
        }

        // ===========================
        // ✅ POST: api/RolesPermisoes
        // ===========================
        [HttpPost]
        public async Task<ActionResult<RolPermisoDto>> PostRolesPermiso(RolPermisoDto dto)
        {
            var rp = new RolesPermiso
            {
                RolId = dto.RolId,
                PermisoId = dto.PermisoId
            };

            _context.RolesPermisos.Add(rp);
            await _context.SaveChangesAsync();

            dto.Id = rp.Id;

            return CreatedAtAction(nameof(GetRolesPermiso), new { id = rp.Id }, dto);
        }

        // ===========================
        // ✅ PUT: api/RolesPermisoes/5
        // ===========================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRolesPermiso(int id, RolPermisoDto dto)
        {
            var rp = await _context.RolesPermisos.FindAsync(id);
            if (rp == null)
                return NotFound();

            rp.RolId = dto.RolId;
            rp.PermisoId = dto.PermisoId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ===========================
        // ✅ DELETE: api/RolesPermisoes/5
        // ===========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolesPermiso(int id)
        {
            var rp = await _context.RolesPermisos.FindAsync(id);
            if (rp == null)
                return NotFound();

            _context.RolesPermisos.Remove(rp);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
