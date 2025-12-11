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
        // ✅ DTO
        // ===========================
        public class ProductoDto
        {
            public int Id { get; set; }
            public string? NombreProducto { get; set; }
            public decimal? Precio { get; set; }
            public int? Stock { get; set; }
            public int? CategoriaId { get; set; }
            // Propiedad que ahora leeremos directamente del producto
            public string? Descripcion { get; set; }
            public int? IdImagen { get; set; }
            public bool? Estado { get; set; }
        }

        // ===========================
        // ✅ GET: api/Productoes (LISTA)
        // ===========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductos()
        {
            var productos = await _context.Productos.ToListAsync();

            return productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                NombreProducto = p.NombreProducto,
                Precio = p.Precio,
                Stock = p.Stock,
                CategoriaId = p.CategoriaId,
                Descripcion = p.Descripcion,
                // [CAMBIO CLAVE AQUI] 
                // Lee directamente la propiedad IdImagen del producto (entidad)
                IdImagen = p.IdImagen,
                Estado = p.Estado

            }).ToList();
        }

        // ===========================
        // ✅ GET: api/Productoes/5 (UN PRODUCTO)
        // ===========================
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            var p = await _context.Productos.FindAsync(id);

            if (p == null)
                return NotFound();

            return new ProductoDto
            {
                Id = p.Id,
                NombreProducto = p.NombreProducto,
                Precio = p.Precio,
                Stock = p.Stock,
                CategoriaId = p.CategoriaId,
                Descripcion = p.Descripcion,

                // [CAMBIO CLAVE AQUI] 
                // Lee directamente la propiedad IdImagen del producto (entidad)
                IdImagen = p.IdImagen,
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
                Descripcion = dto.Descripcion,

                // Asigna directamente IdImagen
                IdImagen = dto.IdImagen,
                Estado = dto.Estado
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            dto.Id = producto.Id;

            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, dto);
        }

        // ===========================
        // ✅ PUT: api/Productoes/5 (ACTUALIZACIÓN)
        // ESTE MÉTODO YA ERA CORRECTO
        // ===========================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, ProductoDto dto)
        {
            // 1. Obtener la entidad para que EF Core la rastree
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound();

            // 2. Mapear/Actualizar propiedades
            producto.NombreProducto = dto.NombreProducto;
            producto.Precio = dto.Precio;
            producto.Stock = dto.Stock;
            producto.CategoriaId = dto.CategoriaId;
            producto.Descripcion = dto.Descripcion;
            producto.IdImagen = dto.IdImagen; // Se mantiene la asignación directa
            producto.Estado = dto.Estado;

            // 3. Persistir los cambios
            await _context.SaveChangesAsync();

            return NoContent(); // Retorna 204
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