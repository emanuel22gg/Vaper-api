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
    public class UsuariosController : ControllerBase
    {
        private readonly VaperContext _context;

        public UsuariosController(VaperContext context)
        {
            _context = context;
        }

        // ===========================
        // ✅ DTO EXACTO SEGÚN TU MODELO
        // ===========================
        public class UsuarioDto
        {
            public int Id { get; set; }
            public string? Nombres { get; set; }
            public string? Apellidos { get; set; }
            public string? Correo { get; set; }
            public string? Contraseña { get; set; }
            public string? TipoDocumento { get; set; }
            public string? NumeroDocumento { get; set; }
            public string? Telefono { get; set; }
            public string? Ciudad { get; set; }
            public string? Direccion { get; set; }
            public string? Barrio { get; set; }
            public DateOnly? FechaNacimiento { get; set; }   // ✅ CORREGIDO
            public bool? EstadoUsuario { get; set; }
            public int? RolId { get; set; }
        }

        // ===========================
        // ✅ GET: api/Usuarios
        // ===========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();

            return usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nombres = u.Nombres,
                Apellidos = u.Apellidos,
                Correo = u.Correo,
                Contraseña = u.Contraseña,
                TipoDocumento = u.TipoDocumento,
                NumeroDocumento = u.NumeroDocumento,
                Telefono = u.Telefono,
                Ciudad = u.Ciudad,
                Direccion = u.Direccion,
                Barrio = u.Barrio,
                FechaNacimiento = u.FechaNacimiento,
                EstadoUsuario = u.EstadoUsuario,
                RolId = u.RolId
            }).ToList();
        }

        // ===========================
        // ✅ GET: api/Usuarios/5
        // ===========================
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuario(int id)
        {
            var u = await _context.Usuarios.FindAsync(id);
            if (u == null)
                return NotFound();

            return new UsuarioDto
            {
                Id = u.Id,
                Nombres = u.Nombres,
                Apellidos = u.Apellidos,
                Correo = u.Correo,
                Contraseña = u.Contraseña,
                TipoDocumento = u.TipoDocumento,
                NumeroDocumento = u.NumeroDocumento,
                Telefono = u.Telefono,
                Ciudad = u.Ciudad,
                Direccion = u.Direccion,
                Barrio = u.Barrio,
                FechaNacimiento = u.FechaNacimiento,
                EstadoUsuario = u.EstadoUsuario,
                RolId = u.RolId
            };
        }

        // ===========================
        // ✅ POST: api/Usuarios
        // ===========================
        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> PostUsuario(UsuarioDto dto)
        {
            var usuario = new Usuario
            {
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                Correo = dto.Correo,
                Contraseña = dto.Contraseña,
                TipoDocumento = dto.TipoDocumento,
                NumeroDocumento = dto.NumeroDocumento,
                Telefono = dto.Telefono,
                Ciudad = dto.Ciudad,
                Direccion = dto.Direccion,
                Barrio = dto.Barrio,
                FechaNacimiento = dto.FechaNacimiento, // ✅ YA ES DateOnly
                EstadoUsuario = dto.EstadoUsuario,
                RolId = dto.RolId
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            dto.Id = usuario.Id;

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, dto);
        }

        // ===========================
        // ✅ PUT: api/Usuarios/5
        // ===========================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioDto dto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            usuario.Nombres = dto.Nombres;
            usuario.Apellidos = dto.Apellidos;
            usuario.Correo = dto.Correo;
            usuario.Contraseña = dto.Contraseña;
            usuario.TipoDocumento = dto.TipoDocumento;
            usuario.NumeroDocumento = dto.NumeroDocumento;
            usuario.Telefono = dto.Telefono;
            usuario.Ciudad = dto.Ciudad;
            usuario.Direccion = dto.Direccion;
            usuario.Barrio = dto.Barrio;
            usuario.FechaNacimiento = dto.FechaNacimiento; // ✅ YA ES DateOnly
            usuario.EstadoUsuario = dto.EstadoUsuario;
            usuario.RolId = dto.RolId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ===========================
        // ✅ DELETE: api/Usuarios/5
        // ===========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
