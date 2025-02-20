using Asp_InnerJoin.Context;
using Asp_InnerJoin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Asp_InnerJoin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Producto2Controller : ControllerBase
    {
        private readonly AppDbContext _context;
        public Producto2Controller(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult<IEnumerable<ProductoEntity>>> Lista()
        {
            var productos = await _context.Productos.ToListAsync();
            return productos;
        }

        [HttpGet]
        [Route("FiltradoNombre/{nombre}")]
        public async Task<ActionResult<IEnumerable<ProductoEntity>>> FiltradoNombre(string nombre)
        {
            /*
            SELECT *
            FROM Productos
            WHERE PROD_NOMBRE = 'nombre'
            */

            var usuarios = await _context.Productos
                .Where(u => u.PROD_NOMBRE == nombre)
                .ToListAsync();

            return usuarios;
        }

        [HttpGet]
        [Route("ProductosConUsuario")]
        public async Task<ActionResult<IEnumerable<ProductoEntity>>> ProductosConUsuario()
        {
            /*
            SELECT p.*, u.*
            FROM Productos p
            INNER JOIN Usuarios u ON p.UsuarioId = u.UsuarioId
            */

            var productosConUsuario = await _context.Productos
                .Include(p => p.Usuario)
                .ToListAsync();

            return productosConUsuario;
        }

        [HttpGet]
        [Route("ProductosPorUsuario")]
        public async Task<ActionResult<IEnumerable>> ProductosPorUsuario()
        {
            /*
            SELECT UsuarioId, COUNT(*) AS TotalProductos
            FROM Productos
            GROUP BY UsuarioId
            */

            var productosPorUsuario = await _context.Productos
                .GroupBy(p => p.ID_USUARIO)
                .Select(g => new
                {
                    UsuarioId = g.Key,
                    TotalProductos = g.Count()
                })
                .ToListAsync();

            return Ok(productosPorUsuario);
        }
    }
}
