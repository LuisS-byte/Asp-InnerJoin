using Asp_InnerJoin.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Asp_InnerJoin.Custom
{
    public class Utilizades
    {
        private readonly IConfiguration _configuration;
        public Utilizades(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public string encriptarSHA256(string clave)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(clave));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }



        public string generarToken(UsuarioEntity usuario)
        {
            var userClaims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.USU_NOMBRE),
                new Claim(ClaimTypes.Email, usuario.USU_EMAIL),
                new Claim(ClaimTypes.Role, usuario.Rol.ROL_NOMBRE)
            };
            var llavasecreta = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            //espesificar el detalle del token
            var credentials = new SigningCredentials(llavasecreta, SecurityAlgorithms.HmacSha256Signature);
            //crear detalle del token
            var jwtConfig = new JwtSecurityToken(
                claims: userClaims,
                //cuando expirará el token
                expires: DateTime.UtcNow.AddMinutes(30),
                //issuer: _configuration["Jwt:Issuer"],
                //audience: _configuration["Jwt:Audience"],
                //credenciales que configuramos arriba
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }
    }
}
