using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Vaper_Api.Models;

namespace Vaper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenesController : ControllerBase
    {
        private readonly VaperContext _context;

        public ImagenesController(VaperContext context)
        {
            _context = context;
        }

        // ===========================
        // ✅ DTO INTERNO
        // ===========================
        public class ImageneDto
        {
            public int IdImagen { get; set; }
            public string? Urlimagen { get; set; }
            public int? ProductoId { get; set; }
        }

        // ===========================
        // ✅ GET - TODAS LAS IMÁGENES
        // ===========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageneDto>>> GetImagenes()
        {
            var imagenes = await _context.Imagenes.ToListAsync();

            return imagenes.Select(i => new ImageneDto
            {
                IdImagen = i.IdImagen,
                Urlimagen = i.Urlimagen,
                ProductoId = i.ProductoId
            }).ToList();
        }

        // ===========================
        // ✅ GET POR ID
        // ===========================
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageneDto>> GetImagene(int id)
        {
            var i = await _context.Imagenes.FindAsync(id);
            if (i == null)
                return NotFound();

            return new ImageneDto
            {
                IdImagen = i.IdImagen,
                Urlimagen = i.Urlimagen,
                ProductoId = i.ProductoId
            };
        }

        // =====================================================
        // ✅ POST NORMAL (CUANDO YA TIENES LA URL)
        // =====================================================
        [HttpPost]
        public async Task<ActionResult<ImageneDto>> PostImagene(ImageneDto dto)
        {
            var imagene = new Imagene
            {
                Urlimagen = dto.Urlimagen,
                ProductoId = dto.ProductoId
            };

            _context.Imagenes.Add(imagene);
            await _context.SaveChangesAsync();

            dto.IdImagen = imagene.IdImagen;

            return CreatedAtAction(nameof(GetImagene), new { id = imagene.IdImagen }, dto);
        }

        // =====================================================
        // ✅ POST CON IMAGEN REAL (SUBE A CLOUDINARY)
        // =====================================================
        [HttpPost("subir")]
        public async Task<ActionResult<ImageneDto>> PostImageneConImagen(
            IFormFile imagen,
            [FromForm] int? productoId,
            [FromServices] CloudinaryService cloudinaryService)
        {
            if (imagen == null)
                return BadRequest("No se envió ninguna imagen");

            // ✅ SUBIR A CLOUDINARY
            var url = await cloudinaryService.UploadImageAsync(imagen);

            if (url == null)
                return BadRequest("Error al subir la imagen");

            // ✅ GUARDAR EN BD
            var imagene = new Imagene
            {
                Urlimagen = url,
                ProductoId = productoId
            };

            _context.Imagenes.Add(imagene);
            await _context.SaveChangesAsync();

            return Ok(new ImageneDto
            {
                IdImagen = imagene.IdImagen,
                Urlimagen = imagene.Urlimagen,
                ProductoId = imagene.ProductoId
            });
        }

        // ===========================
        // ✅ PUT
        // ===========================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImagene(int id, ImageneDto dto)
        {
            var imagene = await _context.Imagenes.FindAsync(id);
            if (imagene == null)
                return NotFound();

            imagene.Urlimagen = dto.Urlimagen;
            imagene.ProductoId = dto.ProductoId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ===========================
        // ✅ DELETE
        // ===========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImagene(int id)
        {
            var imagene = await _context.Imagenes.FindAsync(id);
            if (imagene == null)
                return NotFound();

            _context.Imagenes.Remove(imagene);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}