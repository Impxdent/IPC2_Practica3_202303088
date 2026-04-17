using Microsoft.AspNetCore.Mvc;
using IPC2_Practica3_202303088.Models;
using IPC2_Practica3_202303088.Services;

namespace IPC2_Practica3_202303088.Controllers
{
    public class ProductosController : Controller
    {
        private readonly InventarioService _service = new InventarioService();

        [HttpGet]
        public IActionResult Index() 
        {
            return View(_service.LeerTodo());
        }

        [HttpGet]
        public IActionResult Formulario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(string nombre, string descripcion, string categoria, decimal precio, int cantidadStock, DateTime? fechaVencimiento)
        {
            var productos = _service.LeerTodo();
            int nuevoId = productos.Count > 0 ? productos.Max(p => p.GetId()) + 1 : 1;
            var nuevo = new Producto();
            nuevo.SetId(nuevoId);
            nuevo.SetNombre(nombre);
            nuevo.SetDescripcion(descripcion);
            nuevo.SetCategoria(categoria);
            nuevo.SetPrecio(precio);
            nuevo.SetCantidadStock(cantidadStock);
            nuevo.SetFechaVencimiento(fechaVencimiento);
            
            productos.Add(nuevo);
            _service.GuardarTodo(productos);

            return RedirectToAction("Index", "Inventario"); 
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var productos = _service.LeerTodo();
            var producto = productos.FirstOrDefault(p => p.GetId() == id);
            
            if (producto != null) 
            {
                productos.Remove(producto);
                _service.GuardarTodo(productos);
            }

            return RedirectToAction("Index", "Inventario");
        }
    }
}