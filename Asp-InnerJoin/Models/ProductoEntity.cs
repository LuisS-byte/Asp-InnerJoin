using System.Text.Json.Serialization;

namespace Asp_InnerJoin.Models
{
    public class ProductoEntity
    {
        public int ID_PRODUCTO { get; set; }
        public string PROD_NOMBRE { get; set; }
        public decimal PROD_PRECIO { get; set; }
        public DateTime PROD_FECHA_CREACION { get; set; }
        public int ID_USUARIO { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual UsuarioEntity Usuario { get; set; }
    }
}
