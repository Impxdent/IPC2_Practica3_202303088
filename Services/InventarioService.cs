using System.Text.Json;
using IPC2_Practica3_202303088.Models;

namespace IPC2_Practica3_202303088.Services
{
    public class InventarioService
    {
        private readonly string _path;

        public InventarioService()
        {
            // Buscamos la carpeta raíz del proyecto de forma segura
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            
            // Si estamos dentro de bin/Debug..., retrocedemos para encontrar la raíz del proyecto
            string root = baseDir.Contains("bin") 
                ? baseDir.Split(new[] { "\\bin" }, StringSplitOptions.None)[0] 
                : baseDir;

            _path = Path.Combine(root, "inventario.json");

            // Si el archivo no existe, creamos uno vacío con corchetes [] para evitar errores
            if (!File.Exists(_path))
            {
                File.WriteAllText(_path, "[]");
            }
        }

        public List<Producto> LeerTodo()
        {
            try 
            {
                if (!File.Exists(_path)) return new List<Producto>();
                
                var json = File.ReadAllText(_path);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                
                // Leemos el JSON usando la clase auxiliar DTO
                var listaAuxiliar = JsonSerializer.Deserialize<List<ProductoDTO>>(json, options);
                
                var resultado = new List<Producto>();
                if (listaAuxiliar != null)
                {
                    foreach (var item in listaAuxiliar)
                    {
                        var p = new Producto();
                        // Usamos tus métodos Set manuales
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
            }
            catch (Exception ex)
            {
                // Imprime el error en la consola de depuración por si el JSON está mal formado
                System.Diagnostics.Debug.WriteLine("Error al leer JSON: " + ex.Message);
                return new List<Producto>();
            }
        }

        public void GuardarTodo(List<Producto> productos)
        {
            try
            {
                // Convertimos tus objetos (con Getters) a la clase DTO que el JSON entiende
                var listaParaGuardar = productos.Select(p => new ProductoDTO
                {
                    id = p.GetId(),
                    nombre = p.GetNombre(),
                    categoria = p.GetCategoria(),
                    descripcion = p.GetDescripcion(),
                    precio = p.GetPrecio(),
                    cantidadStock = p.GetCantidadStock(),
                    fechaVencimiento = p.GetFechaVencimiento()
                }).ToList();

                var options = new JsonSerializerOptions { WriteIndented = true };
                var jsonString = JsonSerializer.Serialize(listaParaGuardar, options);
                File.WriteAllText(_path, jsonString);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al guardar JSON: " + ex.Message);
            }
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