using Microsoft.Data.SqlClient;
using ServicioXml.Models;

namespace ServicioXml.Data
{
    public class ProductoRepository
    {
        private readonly string _connectionString;

        public ProductoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Producto> ObtenerTodos()
        {
            List<Producto> productos = new List<Producto>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                // Actualizamos la consulta para pedir todos los ingredientes nuevos
                string query = "SELECT Id, Nombre, Categoria, Precio, Stock FROM Productos";
                
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Producto nuevoProducto = new Producto
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Categoria = reader["Categoria"].ToString(), // ¡Nuevo!
                                Precio = Convert.ToDecimal(reader["Precio"]),
                                Stock = Convert.ToInt32(reader["Stock"])    // ¡Nuevo!
                            };
                            
                            productos.Add(nuevoProducto);
                        }
                    }
                }
            }
            return productos;
        }
    }
}