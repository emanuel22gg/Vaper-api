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
    public class ProductoesController : ControllerBase
    {
        private readonly VaperContext _context;

        public ProductoesController(VaperContext context)
        {
            _context = context;
        }

        // ===========================
        // ✅ DTO INTERNO
        // ===========================
        public class ProductoDto
        {
            public int Id { get; set; }
            public string? NombreProducto { get; set; }
            public decimal? Precio { get; set; }
            public int? Stock { get; set; }
            public int? CategoriaId { get; set; }
            public int? IdImagen { get; set; }
            public string? Urlimagen { get; set; }
            public bool? Estado { get; set; }
        }

        // ===========================
        // ✅ GET: api/Productoes
        // ===========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductos()
        {
            var productos = await _context.Productos
                .Include(p => p.IdImagenNavigation)
                .ToListAsync();

            return productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                NombreProducto = p.NombreProducto,
                Precio = p.Precio,
                Stock = p.Stock,
                CategoriaId = p.CategoriaId,
                IdImagen = p.IdImagen,
                Urlimagen = p.IdImagenNavigation != null ? p.IdImagenNavigation.Urlimagen : null,
                Estado = p.Estado
            }).ToList();
        }

        // ===========================
        // ✅ GET: api/Productoes/5
        // ===========================
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            var p = await _context.Productos
                .Include(x => x.IdImagenNavigation)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (p == null)
                return NotFound();

            return new ProductoDto
            {
                Id = p.Id,
                NombreProducto = p.NombreProducto,
                Precio = p.Precio,
                Stock = p.Stock,
                CategoriaId = p.CategoriaId,
                IdImagen = p.IdImagen,
                Urlimagen = p.IdImagenNavigation != null ? p.IdImagenNavigation.Urlimagen : null,
                Estado = p.Estado
            };
        }

        // ===========================
        // ✅ POST: api/Productoes
        // ===========================
        [HttpPost]
        public async Task<ActionResult<ProductoDto>> PostProducto(ProductoDto dto)
        {
            var producto = new Producto
            {
                NombreProducto = dto.NombreProducto,
                Precio = dto.Precio,
                Stock = dto.Stock,
                CategoriaId = dto.CategoriaId,
                IdImagen = dto.IdImagen,
                Estado = dto.Estado
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            dto.Id = producto.Id;

            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, dto);
        }

        // ===========================
        // ✅ PUT: api/Productoes/5
        // ===========================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, ProductoDto dto)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound();

            producto.NombreProducto = dto.NombreProducto;
            producto.Precio = dto.Precio;
            producto.Stock = dto.Stock;
            producto.CategoriaId = dto.CategoriaId;
            producto.IdImagen = dto.IdImagen;
            producto.Estado = dto.Estado;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ===========================
        // ✅ DELETE: api/Productoes/5
        // ===========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound();

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
