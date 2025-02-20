using System.Text.Json.Serialization;

namespace Asp_InnerJoin.Models
{
    public class UsuarioEntity
    {
        public int ID_USUARIO { get; set; }
        public string USU_NOMBRE { get; set; }
        public string USU_EMAIL { get; set; }
        public DateTime USU_FECHA_REGISTRO { get; set; }

        public int ID_ROL { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual RolEntity Rol { get; set; }
    }
}
