using System.Text.Json.Serialization;

namespace Asp_InnerJoin.Models
{
    public class ProductoEntity
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal PrecioPRoducto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdUsuarioProducto { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual UsuarioEntity Usuario { get; set; }
    }
}
