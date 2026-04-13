using System.Text.Json;
using IPC2_Practica3_202303088.Models;

namespace IPC2_Practica3_202303088.Services
{
    public class InventarioService
    {
        private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "inventario.json");

        public List<Producto> LeerTodo()
        {
            Console.WriteLine($"Buscando archivo en: {Path.GetFullPath(_path)}");
            if (!File.Exists(_path)) 
            {
                Console.WriteLine("¡ERROR: El archivo no existe en esa ruta!");
                return new List<Producto>();
            }
            try 
            {
                if (!File.Exists(_path)) return new List<Producto>();
                
                var json = File.ReadAllText(_path);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var listaAuxiliar = JsonSerializer.Deserialize<List<ProductoDTO>>(json, options);
                
                var resultado = new List<Producto>();
                if (listaAuxiliar != null)
                {
                    foreach (var item in listaAuxiliar)
                    {
                        var p = new Producto();
                        p.SetId(item.id);
                        p.SetNombre(item.nombre);
                        p.SetCategoria(item.categoria);
                        p.SetPrecio(item.precio);
                        p.SetCantidadStock(item.cantidadStock);
                        p.SetFechaVencimiento(item.fechaVencimiento);
                        p.SetDescripcion(item.descripcion);
                        resultado.Add(p);
                    }
                }
                return resultado;
            }
            catch { return new List<Producto>(); }
        }

        public void GuardarTodo(List<Producto> productos)
        {
            var listaParaGuardar = new List<ProductoDTO>();

            foreach (var p in productos)
            {
                listaParaGuardar.Add(new ProductoDTO
                {
                    id = p.GetId(),
                    nombre = p.GetNombre(),
                    categoria = p.GetCategoria(),
                    descripcion = p.GetDescripcion(),
                    precio = p.GetPrecio(),
                    cantidadStock = p.GetCantidadStock(),
                    fechaVencimiento = p.GetFechaVencimiento()
                });
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(listaParaGuardar, options);
            File.WriteAllText(_path, json);
        }

        private class ProductoDTO
        {
            public int id { get; set; }
            public string nombre { get; set; } = "";
            public string categoria { get; set; } = "";
            public string descripcion { get; set; } = "";
            public decimal precio { get; set; }
            public int cantidadStock { get; set; }
            public DateTime? fechaVencimiento { get; set; }
        }
    }
}