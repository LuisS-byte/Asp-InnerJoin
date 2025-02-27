using System.Text.Json.Serialization;

namespace Asp_InnerJoin.Models
{
    public class RolEntity
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ICollection<UsuarioEntity> Usuarios { get; set; }
    }
}
