using Asp_InnerJoin.Context;
using Asp_InnerJoin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp_InnerJoin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductoController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoEntity>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoEntity>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return producto;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, ProductoEntity producto)
        {
            if (id != producto.ID_PRODUCTO)
            {
                return BadRequest();
            }
            _context.Entry(producto).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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
        [HttpPost]
        public async Task<ActionResult<ProductoEntity>> PostProducto(ProductoEntity producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProducto", new { id = producto.ID_PRODUCTO }, producto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.ID_PRODUCTO == id);
        }
    }
}
