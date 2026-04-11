using Microsoft.AspNetCore.Mvc;
using IPC2_Practica3_202303088.Models;

namespace IPC2_Practica3_202303088.Controllers
{
    public class InventarioController : Controller
    {
        public IActionResult Index()
        {
            var listaVacia = new List<Producto>();
            return View(listaVacia);
        }

        public IActionResult Formulario()
        {
            return View();
        }
    }
}