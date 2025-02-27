using Asp_InnerJoin.Context;
using Asp_InnerJoin.Custom;
using Asp_InnerJoin.Models;
using Asp_InnerJoin.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp_InnerJoin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AccesoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Utilizades _utilidades;
        public AccesoController(AppDbContext context, Utilizades utilidades)
        {
            _context = context;
            _utilidades = utilidades;
        }
        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Registrarse(RegistrarseDTO objeto)
        {
            var modeloUsuario = new UsuarioEntity
            {
                NombreUsuario = objeto.NombreUsuario,
                EmailUsuario = objeto.EmailUsuario,
                IdRolUsuario = objeto.IdRolUsuario,

                ClaveUsuario = _utilidades.encriptarSHA256(objeto.ClaveUsuario)
            };

            await _context.Usuarios.AddAsync(modeloUsuario);
            await _context.SaveChangesAsync();
            if (modeloUsuario.IdUsuario != 0)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSucces = true });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSucces = false });
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO objeto)
        {
            var usuarioEncontrado = await _context.Usuarios
                .Include(u => u.Rol)
                .Where(u =>
                    u.EmailUsuario == objeto.Email &&
                    u.ClaveUsuario == _utilidades.encriptarSHA256(objeto.Clave)
                )
                .FirstOrDefaultAsync();

            if (usuarioEncontrado == null)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });

            // Generar el token con el nombre del rol
            var token = _utilidades.generarToken(usuarioEncontrado);

            // Enviar respuesta con el token y el nombre del rol
            return StatusCode(StatusCodes.Status200OK, new
            {
                isSuccess = true,
                token = token,
                rol = usuarioEncontrado.Rol?.NombreRol // Asegurarse de que Rol no sea null
            });
        }

    }
}
