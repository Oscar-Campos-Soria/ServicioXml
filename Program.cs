using System.Xml.Linq;
using ServicioXml.Data;
using ServicioXml.Models;

var builder = WebApplication.CreateBuilder(args);

// --- NUEVO: Permiso de seguridad para que el HTML pueda conectarse sin bloqueos ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", builder => 
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
// ---------------------------------------------------------------------------------

var app = builder.Build();

// Activamos el permiso de seguridad
app.UseCors("PermitirTodo");

app.MapGet("/reporte", () =>
{
    string connectionString = "Server=localhost\\SQLEXPRESS;Database=MiProyecto;Integrated Security=True;TrustServerCertificate=True;";
    ProductoRepository repositorio = new ProductoRepository(connectionString);
    List<Producto> listaProductos = repositorio.ObtenerTodos();

    XElement xmlReporte = new XElement("ReporteProductos",
        new XAttribute("FechaGeneracion", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
    );

    foreach (Producto prod in listaProductos)
    {
        XElement xmlProducto = new XElement("Producto",
            new XElement("Id", prod.Id),
            new XElement("Nombre", prod.Nombre),
            new XElement("Categoria", prod.Categoria),
            new XElement("Precio", prod.Precio),
            new XElement("Stock", prod.Stock)
        );
        xmlReporte.Add(xmlProducto);
    }

    // --- EL ARCHIVO FÍSICO CON EL NOMBRE EXACTO ---
    xmlReporte.Save("Reporte.xml"); 

    return Results.Text(xmlReporte.ToString(), "application/xml");
});

app.Run();