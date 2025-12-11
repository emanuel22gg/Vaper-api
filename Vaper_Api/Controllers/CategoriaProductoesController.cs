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
    public class CategoriaProductoesController : ControllerBase
    {
        private readonly VaperContext _context;

        public CategoriaProductoesController(VaperContext context)
        {
            _context = context;
        }

        // DTO para entrada/salida
        public class CategoriaProductoDto
        {
            public int? Id { get; set; }  // ← AGREGADO
            public string NombreCategoria { get; set; }
            public string? Descripcion { get; set; }
            public bool? Estado { get; set; }

            public int? IdImagen { get; set; }  // ← AGREGADO
        }

        // GET: api/CategoriaProductoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaProductoDto>>> GetCategoriaProductos()
        {
            var categorias = await _context.CategoriaProductos.ToListAsync();

            return categorias.Select(c => new CategoriaProductoDto
            {
                Id = c.Id,  // ← AGREGADO
                NombreCategoria = c.NombreCategoria,
                Descripcion = c.Descripcion,
                Estado = c.Estado,
                IdImagen = c.IdImagen

            }).ToList();
        }

        // GET: api/CategoriaProductoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaProductoDto>> GetCategoriaProducto(int id)
        {
            var c = await _context.CategoriaProductos.FindAsync(id);
            if (c == null) return NotFound();

            return new CategoriaProductoDto
            {
                Id = c.Id,  // ← AGREGADO
                NombreCategoria = c.NombreCategoria,
                Descripcion = c.Descripcion,
                Estado = c.Estado,
                IdImagen = c.IdImagen
            };
        }

        // POST: api/CategoriaProductoes
        [HttpPost]
        public async Task<ActionResult<CategoriaProductoDto>> PostCategoriaProducto(CategoriaProductoDto dto)
        {
            var categoria = new CategoriaProducto
            {
                NombreCategoria = dto.NombreCategoria,
                Descripcion = dto.Descripcion,
                Estado = dto.Estado,
                IdImagen= dto.IdImagen
            };

            _context.CategoriaProductos.Add(categoria);
            await _context.SaveChangesAsync();

            dto.Id = categoria.Id;  // ← AGREGADO: Asignar el Id generado al DTO

            return CreatedAtAction("GetCategoriaProducto", new { id = categoria.Id }, dto);
        }

        // PUT: api/CategoriaProductoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaProducto(int id, CategoriaProductoDto dto)
        {
            var categoria = await _context.CategoriaProductos.FindAsync(id);
            if (categoria == null) return NotFound();

            categoria.NombreCategoria = dto.NombreCategoria;
            categoria.Descripcion = dto.Descripcion;
            categoria.Estado = dto.Estado;
            categoria.IdImagen = dto.IdImagen;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/CategoriaProductoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriaProducto(int id)
        {
            var categoria = await _context.CategoriaProductos.FindAsync(id);
            if (categoria == null) return NotFound();

            _context.CategoriaProductos.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}