using Asp_InnerJoin.Context;
using Asp_InnerJoin.Custom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp_InnerJoin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AccesoController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Registrarse(UsuarioDTO objeto)
        {
            var modeloUsuario = new Usuario
            {
                Nombre = objeto.Nombre,
                Correo = objeto.Correo,
                IdRol = objeto.IdRol,

                Clave = _utilidades.encriptarSHA256(objeto.Clave)
            };

            await _dbPruebaContext.Usuarios.AddAsync(modeloUsuario);
            await _dbPruebaContext.SaveChangesAsync();
            if (modeloUsuario.IdUsuario != 0)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSucces = true });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSucces = false });
            }
        }
    }
}
