using Microsoft.AspNetCore.Mvc;
using IPC2_Practica3_202303088.Models;
using IPC2_Practica3_202303088.Services;

namespace IPC2_Practica3_202303088.Controllers
{
    public class ProductosController : Controller
    {
        private readonly InventarioService _service = new InventarioService();

        public IActionResult Index() => View(_service.LeerTodo());

        public IActionResult Formulario() => View();

        public IActionResult Modificar(int id)
        {
            var producto = _service.LeerTodo().FirstOrDefault(p => p.GetId() == id);
            return producto != null ? View("Formulario", producto) : RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Guardar(int? id, string nombre, string descripcion, string categoria, decimal precio, int cantidadStock, DateTime? fechaVencimiento)
        {
            var productos = _service.LeerTodo();
            
            if (id.HasValue && id > 0) {
                var p = productos.FirstOrDefault(x => x.GetId() == id);
                if (p != null) {
                    p.SetNombre(nombre); p.SetDescripcion(descripcion); p.SetCategoria(categoria);
                    p.SetPrecio(precio); p.SetCantidadStock(cantidadStock); p.SetFechaVencimiento(fechaVencimiento);
                }
            } else {
                var p = new Producto();
                p.SetId(productos.Count > 0 ? productos.Max(x => x.GetId()) + 1 : 1);
                p.SetNombre(nombre); p.SetDescripcion(descripcion); p.SetCategoria(categoria);
                p.SetPrecio(precio); p.SetCantidadStock(cantidadStock); p.SetFechaVencimiento(fechaVencimiento);
                productos.Add(p);
            }

            _service.GuardarTodo(productos);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(int id) {
            var productos = _service.LeerTodo();
            productos.RemoveAll(p => p.GetId() == id);
            _service.GuardarTodo(productos);
            return RedirectToAction("Index");
        }
    }
}