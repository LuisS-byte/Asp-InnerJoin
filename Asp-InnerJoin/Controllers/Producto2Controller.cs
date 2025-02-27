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

            var usuarios = (from p in _context.Productos where p.PROD_NOMBRE == nombre
                            select p).ToList();

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

            var productosConUsuario = await (from p in _context.Productos
                                       join u in _context.Usuarios
                                       on p.ID_USUARIO equals u.ID_USUARIO
                                       select new
                                       {
                                           Id_Producto = p.ID_PRODUCTO,
                                           NombreProducto = p.PROD_NOMBRE,
                                           Usuario = u.USU_NOMBRE
                                       }
                                       ).ToListAsync();
            return Ok(productosConUsuario);
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

        [HttpPost]
        [Route("Crear")]
        public async Task<ActionResult<ProductoEntity>> Crear(ProductoEntity producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProducto", new { id = producto.ID_PRODUCTO }, producto);
        }

    }
}
