using Microsoft.AspNetCore.Mvc;
using IPC2_Practica3_202303088.Models;
using IPC2_Practica3_202303088.Services;

namespace IPC2_Practica3_202303088.Controllers
{
    public class InventarioController : Controller
    {
        private readonly InventarioService _service = new InventarioService();

        public IActionResult Index() 
        {
            return View(_service.LeerTodo());
        }

        public IActionResult Formulario() 
        {
            return View();
        }

        [HttpGet]
        public IActionResult Modificar(int id)
        {
            var productos = _service.LeerTodo();
            var producto = productos.FirstOrDefault(p => p.GetId() == id);

            if (producto == null) 
            {
                return RedirectToAction("Index");
            }
            return View("Modificar", producto); 
        }

        [HttpPost]
        public IActionResult Guardar(int? id, string nombre, string descripcion, string categoria, decimal precio, int cantidadStock, DateTime? fechaVencimiento)
        {
            var productos = _service.LeerTodo();

            if (id.HasValue && id.Value > 0)
            {
                var p = productos.FirstOrDefault(x => x.GetId() == id.Value);
                if (p != null)
                {
                    p.SetNombre(nombre); p.SetDescripcion(descripcion); p.SetCategoria(categoria);
                    p.SetPrecio(precio); p.SetCantidadStock(cantidadStock); p.SetFechaVencimiento(fechaVencimiento);
                }
            }
            else
            {
                var p = new Producto();
                p.SetId(productos.Count > 0 ? productos.Max(x => x.GetId()) + 1 : 1);
                p.SetNombre(nombre); p.SetDescripcion(descripcion); p.SetCategoria(categoria);
                p.SetPrecio(precio); p.SetCantidadStock(cantidadStock); p.SetFechaVencimiento(fechaVencimiento);
                productos.Add(p);
            }

            _service.GuardarTodo(productos);
            return RedirectToAction("Index");
        }
    }
}