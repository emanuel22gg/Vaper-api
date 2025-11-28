using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vaper_Api.Models;

namespace Vaper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleVentaPedidoesController : ControllerBase
    {
        private readonly VaperContext _context;

        public DetalleVentaPedidoesController(VaperContext context)
        {
            _context = context;
        }

        // GET: api/DetalleVentaPedidoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleVentaPedido>>> GetDetalleVentaPedidos()
        {
            return await _context.DetalleVentaPedidos.ToListAsync();
        }

        // GET: api/DetalleVentaPedidoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleVentaPedido>> GetDetalleVentaPedido(int id)
        {
            var detalleVentaPedido = await _context.DetalleVentaPedidos.FindAsync(id);

            if (detalleVentaPedido == null)
            {
                return NotFound();
            }

            return detalleVentaPedido;
        }

        // PUT: api/DetalleVentaPedidoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleVentaPedido(int id, DetalleVentaPedido detalleVentaPedido)
        {
            if (id != detalleVentaPedido.Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleVentaPedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleVentaPedidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DetalleVentaPedidoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleVentaPedido>> PostDetalleVentaPedido(DetalleVentaPedido detalleVentaPedido)
        {
            _context.DetalleVentaPedidos.Add(detalleVentaPedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalleVentaPedido", new { id = detalleVentaPedido.Id }, detalleVentaPedido);
        }

        // DELETE: api/DetalleVentaPedidoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleVentaPedido(int id)
        {
            var detalleVentaPedido = await _context.DetalleVentaPedidos.FindAsync(id);
            if (detalleVentaPedido == null)
            {
                return NotFound();
            }

            _context.DetalleVentaPedidos.Remove(detalleVentaPedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleVentaPedidoExists(int id)
        {
            return _context.DetalleVentaPedidos.Any(e => e.Id == id);
        }
    }
}
