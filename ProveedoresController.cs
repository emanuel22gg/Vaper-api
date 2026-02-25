Vaper_Api\Controllers\ProveedoresController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vaper_Api.Models;
using System.Text.Json;

namespace Vaper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly VaperContext _context;

        public ProveedoresController(VaperContext context)
        {
            _context = context;
        }

        // DTO para controlar la entrada/salida
        public class ContactoAdicionalDto
        {
            public string? Nombre { get; set; }
            public string? Cargo { get; set; }
            public string? Telefono { get; set; }
            public string? Email { get; set; }
        }

        public class ProveedorDto
        {
            public int? Id { get; set; }
            public string? Codigo { get; set; }
            public string? TipoPersona { get; set; }
            public string? Nombres { get; set; }
            public string? Apellidos { get; set; }
            public string? Cedula { get; set; }
            public string? RazonSocial { get; set; }
            public string? Nit { get; set; }
            public string? Celular { get; set; }
            public string? Pais { get; set; }
            public string[]? Productos { get; set; }
            public DateTime? FechaRegistro { get; set; }
            public DateTime? UltimaCompra { get; set; }
            public decimal? TotalCompras { get; set; }
            public string? Banco { get; set; }
            public string? NumeroCuenta { get; set; }
            public string? TipoCuenta { get; set; }
            public ContactoAdicionalDto? ContactoAdicional { get; set; }
            public string? Observaciones { get; set; }

            // Campos legacy existentes
            public string? NombreCompletoORazonSocial { get; set; }
            public string? TipoDocumento { get; set; }
            public string? NumeroDocumento { get; set; }
            public string? RepresentanteLegal { get; set; }
            public string? Email { get; set; }
            public string? Telefono { get; set; }
            public string? Direccion { get; set; }
            public string? Ciudad { get; set; }
            public bool Estado { get; set; }
        }

        // GET: api/Proveedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorDto>>> GetProveedores()
        {
            var proveedores = await _context.Proveedores.ToListAsync();

            return proveedores.Select(p => new ProveedorDto
            {
                Id = p.Id,
                Codigo = p.Codigo,
                TipoPersona = p.TipoPersona,
                Nombres = p.Nombres,
                Apellidos = p.Apellidos,
                Cedula = p.Cedula,
                RazonSocial = p.RazonSocial,
                Nit = p.Nit,
                Celular = p.Celular,
                Pais = p.Pais,
                Productos = string.IsNullOrEmpty(p.Productos) ? Array.Empty<string>() : JsonSerializer.Deserialize<string[]>(p.Productos),
                FechaRegistro = p.FechaRegistro,
                UltimaCompra = p.UltimaCompra,
                TotalCompras = p.TotalCompras,
                Banco = p.Banco,
                NumeroCuenta = p.NumeroCuenta,
                TipoCuenta = p.TipoCuenta,
                ContactoAdicional = string.IsNullOrEmpty(p.ContactoAdicional) ? null : JsonSerializer.Deserialize<ContactoAdicionalDto>(p.ContactoAdicional),
                Observaciones = p.Observaciones,

                NombreCompletoORazonSocial = p.NombreCompletoORazonSocial,
                TipoDocumento = p.TipoDocumento,
                NumeroDocumento = p.NumeroDocumento,
                RepresentanteLegal = p.RepresentanteLegal,
                Email = p.Email,
                Telefono = p.Telefono,
                Direccion = p.Direccion,
                Ciudad = p.Ciudad,
                Estado = p.Estado
            }).ToList();
        }

        // GET: api/Proveedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProveedorDto>> GetProveedor(int id)
        {
            var p = await _context.Proveedores.FindAsync(id);
            if (p == null) return NotFound();

            return new ProveedorDto
            {
                Id = p.Id,
                Codigo = p.Codigo,
                TipoPersona = p.TipoPersona,
                Nombres = p.Nombres,
                Apellidos = p.Apellidos,
                Cedula = p.Cedula,
                RazonSocial = p.RazonSocial,
                Nit = p.Nit,
                Celular = p.Celular,
                Pais = p.Pais,
                Productos = string.IsNullOrEmpty(p.Productos) ? Array.Empty<string>() : JsonSerializer.Deserialize<string[]>(p.Productos),
                FechaRegistro = p.FechaRegistro,
                UltimaCompra = p.UltimaCompra,
                TotalCompras = p.TotalCompras,
                Banco = p.Banco,
                NumeroCuenta = p.NumeroCuenta,
                TipoCuenta = p.TipoCuenta,
                ContactoAdicional = string.IsNullOrEmpty(p.ContactoAdicional) ? null : JsonSerializer.Deserialize<ContactoAdicionalDto>(p.ContactoAdicional),
                Observaciones = p.Observaciones,

                NombreCompletoORazonSocial = p.NombreCompletoORazonSocial,
                TipoDocumento = p.TipoDocumento,
                NumeroDocumento = p.NumeroDocumento,
                RepresentanteLegal = p.RepresentanteLegal,
                Email = p.Email,
                Telefono = p.Telefono,
                Direccion = p.Direccion,
                Ciudad = p.Ciudad,
                Estado = p.Estado
            };
        }

        // POST: api/Proveedores
        [HttpPost]
        public async Task<ActionResult<ProveedorDto>> PostProveedor(ProveedorDto dto)
        {
            var proveedor = new Proveedore
            {
                Codigo = dto.Codigo,
                TipoPersona = dto.TipoPersona,
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                Cedula = dto.Cedula,
                RazonSocial = dto.RazonSocial,
                Nit = dto.Nit,
                Celular = dto.Celular,
                Pais = dto.Pais,
                Productos = dto.Productos == null ? null : JsonSerializer.Serialize(dto.Productos),
                FechaRegistro = dto.FechaRegistro,
                UltimaCompra = dto.UltimaCompra,
                TotalCompras = dto.TotalCompras,
                Banco = dto.Banco,
                NumeroCuenta = dto.NumeroCuenta,
                TipoCuenta = dto.TipoCuenta,
                ContactoAdicional = dto.ContactoAdicional == null ? null : JsonSerializer.Serialize(dto.ContactoAdicional),
                Observaciones = dto.Observaciones,

                NombreCompletoORazonSocial = dto.NombreCompletoORazonSocial,
                TipoDocumento = dto.TipoDocumento,
                NumeroDocumento = dto.NumeroDocumento,
                RepresentanteLegal = dto.RepresentanteLegal,
                Email = dto.Email,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                Ciudad = dto.Ciudad,
                Estado = dto.Estado
            };

            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            dto.Id = proveedor.Id;

            return CreatedAtAction(nameof(GetProveedor), new { id = proveedor.Id }, dto);
        }

        // PUT: api/Proveedores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, ProveedorDto dto)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null) return NotFound();

            proveedor.Codigo = dto.Codigo;
            proveedor.TipoPersona = dto.TipoPersona;
            proveedor.Nombres = dto.Nombres;
            proveedor.Apellidos = dto.Apellidos;
            proveedor.Cedula = dto.Cedula;
            proveedor.RazonSocial = dto.RazonSocial;
            proveedor.Nit = dto.Nit;
            proveedor.Celular = dto.Celular;
            proveedor.Pais = dto.Pais;
            proveedor.Productos = dto.Productos == null ? null : JsonSerializer.Serialize(dto.Productos);
            proveedor.FechaRegistro = dto.FechaRegistro;
            proveedor.UltimaCompra = dto.UltimaCompra;
            proveedor.TotalCompras = dto.TotalCompras;
            proveedor.Banco = dto.Banco;
            proveedor.NumeroCuenta = dto.NumeroCuenta;
            proveedor.TipoCuenta = dto.TipoCuenta;
            proveedor.ContactoAdicional = dto.ContactoAdicional == null ? null : JsonSerializer.Serialize(dto.ContactoAdicional);
            proveedor.Observaciones = dto.Observaciones;

            proveedor.NombreCompletoORazonSocial = dto.NombreCompletoORazonSocial;
            proveedor.TipoDocumento = dto.TipoDocumento;
            proveedor.NumeroDocumento = dto.NumeroDocumento;
            proveedor.RepresentanteLegal = dto.RepresentanteLegal;
            proveedor.Email = dto.Email;
            proveedor.Telefono = dto.Telefono;
            proveedor.Direccion = dto.Direccion;
            proveedor.Ciudad = dto.Ciudad;
            proveedor.Estado = dto.Estado;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Proveedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null) return NotFound();

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}