using System.Text.Json.Serialization;

namespace Asp_InnerJoin.Models
{
    public class UsuarioEntity
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public DateTime FechaRegistro { get; set; }

        public string ClaveUsuario { get; set; }
        public int IdRolUsuario { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual RolEntity Rol { get; set; }
    }
}
