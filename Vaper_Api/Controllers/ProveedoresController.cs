using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vaper_Api.Models;

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
        public class ProveedorDto
        {
            public int? Id { get; set; }
            public string NombreCompletoORazonSocial { get; set; }
            public string? TipoDocumento { get; set; }
            public string NumeroDocumento { get; set; }
            public string? RepresentanteLegal { get; set; }
            public string? Email { get; set; }
            public string? Telefono { get; set; }
            public string? Direccion { get; set; }
            public string? Ciudad { get; set; }
            public string? Estado { get; set; }
        }

        // GET: api/Proveedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorDto>>> GetProveedores()
        {
            var proveedores = await _context.Proveedores.ToListAsync();

            return proveedores.Select(p => new ProveedorDto
            {
                Id = p.Id,
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
