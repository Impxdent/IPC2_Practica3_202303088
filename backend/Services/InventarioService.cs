using System.Text.Json;
using backend.Models;

namespace backend.Services
{
    public class InventarioService
    {
        private readonly string _path;

        public InventarioService()
        {
            _path = Path.Combine(Directory.GetCurrentDirectory(), "inventario.json");

            if (!File.Exists(_path))
                File.WriteAllText(_path, "[]");
        }

        public List<Producto> LeerTodo()
        {
            try
            {
                var json = File.ReadAllText(_path);

                var listaDTO = JsonSerializer.Deserialize<List<ProductoDTO>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var resultado = new List<Producto>();

                if (listaDTO != null)
                {
                    foreach (var item in listaDTO)
                    {
                        var p = new Producto
                        {
                            Id = item.id,
                            Nombre = item.nombre,
                            Categoria = item.categoria,
                            Descripcion = item.descripcion,
                            Precio = item.precio,
                            CantidadStock = item.cantidadStock,
                            FechaVencimiento = item.fechaVencimiento
                        };

                        resultado.Add(p);
                    }
                }

                return resultado;
            }
            catch
            {
                return new List<Producto>();
            }
        }

        public void GuardarTodo(List<Producto> productos)
        {
            var dto = productos.Select(p => new ProductoDTO
            {
                id = p.Id,
                nombre = p.Nombre,
                categoria = p.Categoria,
                descripcion = p.Descripcion,
                precio = p.Precio,
                cantidadStock = p.CantidadStock,
                fechaVencimiento = p.FechaVencimiento
            }).ToList();

            File.WriteAllText(_path,
                JsonSerializer.Serialize(dto, new JsonSerializerOptions { WriteIndented = true }));
        }

        private class ProductoDTO
        {
            public int id { get; set; }
            public string nombre { get; set; }
            public string categoria { get; set; }
            public string descripcion { get; set; }
            public decimal precio { get; set; }
            public int cantidadStock { get; set; }
            public DateTime? fechaVencimiento { get; set; }
        }
    }
}