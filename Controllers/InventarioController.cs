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
            var productos = _service.LeerTodo();

            if (productos.Count == 0)
            {
                var pPrueba = new Producto();
                pPrueba.SetNombre("Producto de Error (JSON vacío o no leído)");
                pPrueba.SetCategoria("Debug");
                pPrueba.SetPrecio(0);
                productos.Add(pPrueba);
            }

            return View(productos);
        }

        public IActionResult Formulario() => View();
    }
}