using System.Text.Json.Serialization;

namespace Asp_InnerJoin.Models
{
    public class RolEntity
    {
        public int ID_ROL { get; set; }
        public string ROL_NOMBRE { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ICollection<UsuarioEntity> Usuarios { get; set; }
    }
}
