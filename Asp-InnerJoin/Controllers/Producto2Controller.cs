using Asp_InnerJoin.Context;
using Asp_InnerJoin.Models;
using Asp_InnerJoin.Models.DTOs;
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


        [HttpGet]
        [Route("ProductosConUsuarioYrol")]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> ProductosConUsuarioYrol()
        {


            /*
            SELECT 
                p.ID_PRODUCTO,
                p.PROD_NOMBRE,
                p.PROD_PRECIO,
                p.PROD_FECHA_CREACION,
                u.USU_NOMBRE AS UsuarioNombre,
                r.ROL_NOMBRE AS RolNombre
            FROM 
                Productos p
            INNER JOIN 
                Usuarios u ON p.ID_USUARIO = u.ID_USUARIO
            INNER JOIN 
                Roles r ON u.ID_ROL = r.ID_ROL;
 
            */


            var productosDTO = await _context.Productos
                .Include(p => p.Usuario)
                .ThenInclude(u => u.Rol) 
                .Select(p => new ProductoDTO
                {
                    ID_PRODUCTO = p.ID_PRODUCTO,
                    PROD_NOMBRE = p.PROD_NOMBRE,
                    PROD_PRECIO = p.PROD_PRECIO,
                    PROD_FECHA_CREACION = p.PROD_FECHA_CREACION,
                    ID_USUARIO = p.ID_USUARIO,
                    UsuarioNombre = p.Usuario.USU_NOMBRE,
                    Id_ROl = p.Usuario.ID_ROL,
                    RolNombre = p.Usuario.Rol.ROL_NOMBRE
                })
                .ToListAsync();

            return Ok(productosDTO);
        }

        [HttpGet]
        [Route("ProductosConUsuarioEmailYrol")]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> ProductosConUsuarioEmailYrol()
        {


            /*
            SELECT 
             p.ID_PRODUCTO,
              p.PROD_NOMBRE,
              p.PROD_PRECIO,
              p.PROD_FECHA_CREACION,
              p.ID_USUARIO,
              u.USU_NOMBRE AS UsuarioNombre,
              u.USU_EMAIL AS UsuarioEmail,
              u.ID_ROL AS Id_Rol,
              r.ROL_NOMBRE AS RolNombre
            FROM 
              Productos p
            INNER JOIN 
              Usuarios u ON p.ID_USUARIO = u.ID_USUARIO
            INNER JOIN 
                Roles r ON u.ID_ROL = r.ID_ROL;

 
            */


            var productosDTO = await _context.Productos
                .Include(p => p.Usuario)
                .ThenInclude(u => u.Rol)
                .Select(p => new ProductoUsuarioEmailDTO
                {
                    ID_PRODUCTO = p.ID_PRODUCTO,
                    PROD_NOMBRE = p.PROD_NOMBRE,
                    PROD_PRECIO = p.PROD_PRECIO,
                    PROD_FECHA_CREACION = p.PROD_FECHA_CREACION,
                    ID_USUARIO=p.ID_USUARIO,    
                    UsuarioNombre = p.Usuario.USU_NOMBRE,
                    UsuarioEmail = p.Usuario.USU_EMAIL,
                    Id_ROl=p.Usuario.ID_ROL,
                    RolNombre = p.Usuario.Rol.ROL_NOMBRE
                })
                .ToListAsync();

            return Ok(productosDTO);
        }



        [HttpGet]
        [Route("ProductosConUsuarioEmailYrolWithIdProducto/{id}")]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> ProductosConUsuarioEmailYrolWithIdProducto(int id)
        {


            /*
            SELECT 
                p.ID_PRODUCTO,
                p.PROD_NOMBRE,
                p.PROD_PRECIO,
                p.PROD_FECHA_CREACION,
                p.ID_USUARIO,
                u.USU_NOMBRE AS UsuarioNombre,
                u.USU_EMAIL AS UsuarioEmail,
                u.ID_ROL AS Id_Rol,
                r.ROL_NOMBRE AS RolNombre
            FROM 
                Productos p
            INNER JOIN 
                Usuarios u ON p.ID_USUARIO = u.ID_USUARIO
            INNER JOIN 
                Roles r ON u.ID_ROL = r.ID_ROL
            WHERE 
                p.ID_PRODUCTO = @id;
            */


            var productosDTO = await _context.Productos
                .Include(p => p.Usuario)
                .ThenInclude(u => u.Rol)
                .Select(p => new ProductoUsuarioEmailDTO
                {
                    ID_PRODUCTO = p.ID_PRODUCTO,
                    PROD_NOMBRE = p.PROD_NOMBRE,
                    PROD_PRECIO = p.PROD_PRECIO,
                    PROD_FECHA_CREACION = p.PROD_FECHA_CREACION,
                    ID_USUARIO = p.ID_USUARIO,
                    UsuarioNombre = p.Usuario.USU_NOMBRE,
                    UsuarioEmail = p.Usuario.USU_EMAIL,
                    Id_ROl = p.Usuario.ID_ROL,
                    RolNombre = p.Usuario.Rol.ROL_NOMBRE
                })
                .Where(i => i.ID_PRODUCTO == id)
                .ToListAsync();

            return Ok(productosDTO);
        }
    }
}
