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
            string rutaReal = Path.GetFullPath("inventario.json");
            ViewBag.RutaDeteccion = rutaReal;
            ViewData["Ruta"] = rutaReal;

            var productos = _service.LeerTodo();
            return View(productos);
        }

        public IActionResult Formulario() => View();
    }
}