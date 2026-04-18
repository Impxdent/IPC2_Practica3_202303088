using System.Text.Json;
using IPC2_Practica3_202303088.Models;

namespace IPC2_Practica3_202303088.Services
{
    public class InventarioService
    {
        private readonly string _path;

        public InventarioService()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string root = baseDir.Contains("bin") ? baseDir.Split(new[] { "\\bin" }, StringSplitOptions.None)[0] : baseDir;
            _path = Path.Combine(root, "inventario.json");

            if (!File.Exists(_path)) File.WriteAllText(_path, "[]");
        }

        public List<Producto> LeerTodo()
        {
            try {
                var json = File.ReadAllText(_path);
                var listaDTO = JsonSerializer.Deserialize<List<ProductoDTO>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var resultado = new List<Producto>();
                if (listaDTO != null) {
                    foreach (var item in listaDTO) {
                        var p = new Producto();
                        p.SetId(item.id); 
                        p.SetNombre(item.nombre); 
                        p.SetCategoria(item.categoria);
                        p.SetDescripcion(item.descripcion); 
                        p.SetPrecio(item.precio);
                        p.SetCantidadStock(item.cantidadStock); 
                        p.SetFechaVencimiento(item.fechaVencimiento);
                        resultado.Add(p);
                    }
                }
                return resultado;
            } catch { return new List<Producto>(); }
        }

        public void GuardarTodo(List<Producto> productos)
        {
            var dto = productos.Select(p => new ProductoDTO {
                id = p.GetId(), 
                nombre = p.GetNombre(), 
                categoria = p.GetCategoria(),
                descripcion = p.GetDescripcion(), 
                precio = p.GetPrecio(),
                cantidadStock = p.GetCantidadStock(), 
                fechaVencimiento = p.GetId() != 0 ? p.GetFechaVencimiento() : null
            }).ToList();
            File.WriteAllText(_path, JsonSerializer.Serialize(dto, new JsonSerializerOptions { WriteIndented = true }));
        }

        private class ProductoDTO {
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