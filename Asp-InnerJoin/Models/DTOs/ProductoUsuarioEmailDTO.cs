namespace Asp_InnerJoin.Models.DTOs
{
    public class ProductoUsuarioEmailDTO
    {
        public int ID_PRODUCTO { get; set; }
        public string PROD_NOMBRE { get; set; }
        public decimal PROD_PRECIO { get; set; }
        public DateTime PROD_FECHA_CREACION { get; set; }
        public int ID_USUARIO { get; set; }

        public string UsuarioNombre { get; set; }

        public string UsuarioEmail { get; set; }
        public int Id_ROl { get; set; }

        public string RolNombre { get; set; }
    }
}
