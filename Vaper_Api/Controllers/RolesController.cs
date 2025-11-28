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
    public class RolesController : ControllerBase
    {
        private readonly VaperContext _context;

        public RolesController(VaperContext context)
        {
            _context = context;
        }

        // ===========================
        // ✅ DTO INTERNO
        // ===========================
        public class RolDto
        {
            public int Id { get; set; }
            public string? NombreRol { get; set; }
            public string? Descripcion { get; set; }
            public bool? EstadoRol { get; set; }
        }

        // ===========================
        // ✅ GET: api/Roles
        // ===========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolDto>>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();

            return roles.Select(r => new RolDto
            {
                Id = r.Id,
                NombreRol = r.NombreRol,
                Descripcion = r.Descripcion,
                EstadoRol = r.EstadoRol
            }).ToList();
        }

        // ===========================
        // ✅ GET: api/Roles/5
        // ===========================
        [HttpGet("{id}")]
        public async Task<ActionResult<RolDto>> GetRole(int id)
        {
            var r = await _context.Roles.FindAsync(id);
            if (r == null)
                return NotFound();

            return new RolDto
            {
                Id = r.Id,
                NombreRol = r.NombreRol,
                Descripcion = r.Descripcion,
                EstadoRol = r.EstadoRol
            };
        }

        // ===========================
        // ✅ POST: api/Roles
        // ===========================
        [HttpPost]
        public async Task<ActionResult<RolDto>> PostRole(RolDto dto)
        {
            var role = new Role
            {
                NombreRol = dto.NombreRol,
                Descripcion = dto.Descripcion,
                EstadoRol = dto.EstadoRol
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            dto.Id = role.Id;

            return CreatedAtAction(nameof(GetRole), new { id = role.Id }, dto);
        }

        // ===========================
        // ✅ PUT: api/Roles/5
        // ===========================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, RolDto dto)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return NotFound();

            role.NombreRol = dto.NombreRol;
            role.Descripcion = dto.Descripcion;
            role.EstadoRol = dto.EstadoRol;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ===========================
        // ✅ DELETE: api/Roles/5
        // ===========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return NotFound();

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
