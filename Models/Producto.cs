namespace ServicioXml.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; } // ¡Nuevo!
        public decimal Precio { get; set; }
        public int Stock { get; set; }        // ¡Nuevo!
    }
}